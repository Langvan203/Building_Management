using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

using BuildingManagement.Application.Interfaces.Services.Ultility;
using BuildingManagement.Domain.Ultility;

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

        public async Task SendEmailWithAtachFileAsync(string to, string subject, string htmlContent, string filePath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSettings.From));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlContent
            };

            builder.Attachments.Add(filePath);
            message.Body = builder.ToMessageBody();

            await SendEmailInternalAsync(message, cancellationToken);

        }
        private async Task SendEmailInternalAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            using var client = new SmtpClient();

            try
            {
                var secureSocket = _emailSettings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, secureSocket, cancellationToken);

                if (!string.IsNullOrEmpty(_emailSettings.UserName) && !string.IsNullOrEmpty(_emailSettings.Password))
                {
                    await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password, cancellationToken);
                }

                await client.SendAsync(message, cancellationToken);
            }
            finally
            {
                await client.DisconnectAsync(true, cancellationToken);
            }
        }

        public async Task SendEmailWithAttachFileAsync(string to, string subject, string htmlContent, IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty");

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSettings.From));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlContent
            };

            // Thêm file đính kèm từ IFormFile
            using (var stream = file.OpenReadStream())
            {
                await builder.Attachments.AddAsync(file.FileName, stream, ContentType.Parse(file.ContentType ?? "application/octet-stream"), cancellationToken);
            }

            message.Body = builder.ToMessageBody();

            await SendEmailInternalAsync(message, cancellationToken);
        }

    }
}
