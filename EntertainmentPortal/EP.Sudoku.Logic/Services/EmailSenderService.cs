using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EP.Sudoku.Logic.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IOptions<EmailSettings> emailSettings, ILogger<EmailSenderService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);

                var mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Port = _emailSettings.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailSettings.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };
        
                client.Send(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registration failed...\n{ex.Message}");
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
