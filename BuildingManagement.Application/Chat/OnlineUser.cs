using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagement.Application.Chat
{
    public class OnlineUser
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public List<string> Role { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
