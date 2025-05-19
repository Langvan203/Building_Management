using BuildingManagement.Domain.Ultility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using BuildingManagement.Application.Interfaces.Services.Ultility;

namespace BuildingManagement.Application.Services.Ultility
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent, CancellationToken cancellationToken = default)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSettings.From));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            var builder = new BodyBuilder
            {
                HtmlBody = htmlContent
            };

            message.Body = builder.ToMessageBody();
            using var client = new MailKit.Net.Smtp.SmtpClient();
            var secureSocket = _emailSettings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, secureSocket, cancellationToken);
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password, cancellationToken);

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }

    }
}
