
using System.Collections.Concurrent;

using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace BuildingManagement.Application.Chat
{
    public class ChatHub: Hub
    {
        private static readonly ConcurrentDictionary<string, OnlineUser> OnelineUsers = new();
        private static readonly ConcurrentDictionary<string, List<string>> UserConnections = new();
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var userName = Context.User?.FindFirst(ClaimTypes.Name)?.Value;
            var clientId = Context.ConnectionId;
            var userRole = Context.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            var userInfor = Context.User;
            var connectionId = Context.ConnectionId;

            var userOneline = new OnlineUser
            {
                UserId = userId,
                ConnectionId = clientId,
                UserName = userName ?? "Anonymous",
                Role = userRole ?? null,
                ConnectedAt = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow
            };

            // Thêm user vào danh sách online users
            OnelineUsers.TryAdd(connectionId, userOneline);
            UserConnections.AddOrUpdate(userId,
               new List<string> { connectionId },
               (key, existingConnections) =>
               {
                   existingConnections.Add(connectionId);
                   return existingConnections;
               });

            if (userRole.Contains("User") || userRole.Contains("Cư dân"))
            {
                await NotifyStaffAboutUserOnline(userName, userId);
            }

            // Gửi danh sách staff online cho user
            if (userRole.Contains("User") || userRole.Contains("Resident"))
            {
                await SendOnlineStaffList();
            }

            // Gửi thông báo kết nối thành công về client
            await Clients.Caller.SendAsync("Connected", "Đã kết nối thành công!", connectionId);
            await base.OnConnectedAsync();
        }


        private async Task SendOnlineStaffList()
        {
            var onlineStaff = OnelineUsers.Values
                .Where(u => u.Role.Contains("Admin") || u.Role.Contains("Technician"))
                .GroupBy(u => u.UserId)
                .Select(g => g.First())
                .Select(u => new
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Role = u.Role,
                    ConnectedAt = u.ConnectedAt
                });

            await Clients.Caller.SendAsync("OnlineStaffList", onlineStaff);
        }
        private async Task NotifyStaffAboutUserOnline(string userName, string userId)
        {
            var staffConnections = GetStaffConnections();
            foreach (var connectionId in staffConnections)
            {
                await Clients.Client(connectionId).SendAsync("UserOnline", new
                {
                    UserId = userId,
                    UserName = userName,
                    ConnectedAt = DateTime.Now
                });
            }
        }

        private List<string> GetStaffConnections()
        {
            return OnelineUsers.Values
                .Where(u => u.Role.Contains("Cư dân") || u.Role.Contains("Technician"))
                .Select(u => u.ConnectionId)
                .ToList();
        }

        private List<string> GetUserConnections()
        {
            return OnelineUsers.Values
                .Where(u => u.Role.Contains("User") || u.Role.Contains("Resident"))
                .Select(u => u.ConnectionId)
                .ToList();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            if (OnelineUsers.TryRemove(connectionId, out var user))
            {
                // Remove connection from user's connection list
                if (UserConnections.TryGetValue(user.UserId, out var connections))
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        UserConnections.TryRemove(user.UserId, out _);

                        // Thông báo user offline nếu không còn connection nào
                        if (user.Role.Contains("User") || user.Role.Contains("Resident"))
                        {
                            await NotifyStaffAboutUserOffline(user.UserName, user.UserId);
                        }
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
        private async Task NotifyStaffAboutUserOffline(string userName, string userId)
        {
            var staffConnections = GetStaffConnections();
            foreach (var connectionId in staffConnections)
            {
                await Clients.Client(connectionId).SendAsync("UserOffline", new
                {
                    UserId = userId,
                    UserName = userName,
                    DisconnectedAt = DateTime.Now
                });
            }
        }

        public async Task TestConnection(string message)
        {
            // Gửi tin nhắn phản hồi lại cho client gọi hàm này
            await Clients.Caller.SendAsync("ReceiveTestMessage", "Server", $"Received your message: {message}");
        }
        public async Task SendMessage(string message)
        {
            var userId = Context.UserIdentifier;
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var clientId = Context.ConnectionId;

            // Kiểm tra tin nhắn đặc biệt
            if (message.ToLower().Contains("xin chào server"))
            {
                // Gửi phản hồi đặc biệt về client gửi tin nhắn
                await Clients.Caller.SendAsync("ReceiveMessage",
                    "Server",
                    $"Xin chào client + {clientId}",
                    DateTime.Now);
            }
            else
            {
                // Gửi tin nhắn đến tất cả client (bao gồm cả người gửi)
                await Clients.All.SendAsync("ReceiveMessage",
                    userName,
                    message,
                    DateTime.Now);
            }
        }

        public async Task SendMessageToUser(string targetUserId, string message)
        {
            var senderName = Context.User?.Identity?.Name ?? "Anonymous";

            await Clients.User(targetUserId).SendAsync("ReceivePrivateMessage",
                senderName,
                message,
                DateTime.Now);
        }

        // Method để join vào một group (có thể dùng cho chat theo tòa nhà/tầng)
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("SystemMessage",
                $"{Context.User?.Identity?.Name} đã tham gia nhóm {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("SystemMessage",
                $"{Context.User?.Identity?.Name} đã rời khỏi nhóm {groupName}");
        }

        // Method để gửi tin nhắn đến một group
        public async Task SendMessageToGroup(string groupName, string message)
        {
            var senderName = Context.User?.Identity?.Name ?? "Anonymous";

            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage",
                groupName,
                senderName,
                message,
                DateTime.Now);
        }
    }
}
