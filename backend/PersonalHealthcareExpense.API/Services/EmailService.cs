using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using PersonalHealthcareExpense.API.Interfaces;

namespace PersonalHealthcareExpense.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var smtpSection = _configuration.GetSection("SmtpSettings");

            var host = smtpSection["Host"] ?? "smtp.gmail.com";
            var port = int.Parse(smtpSection["Port"] ?? "587");
            var userName = smtpSection["UserName"] ?? "";
            var password = smtpSection["Password"] ?? "";
            var fromEmail = smtpSection["FromEmail"] ?? userName;
            var fromName = smtpSection["FromName"] ?? "Healthcare Tracker";

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true,
                Timeout = 30000
            };

            await client.SendMailAsync(message);
        }
    }
}
