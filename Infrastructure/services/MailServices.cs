using Application.services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Timers;

namespace Infrastructure.services
{
    public class MailServices : IMailServices
    {
        private readonly IConfiguration _configuration;

        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string receiver, string subject, string body)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(receiver));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["MailSettings:Host"], Convert.ToInt32(_configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }



    }
}
