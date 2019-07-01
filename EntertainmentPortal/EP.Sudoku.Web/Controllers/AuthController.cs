using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;

namespace EP.Sudoku.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _manager;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<IdentityUser> manager, IMediator mediator, IEmailSender emailSender)
        {
            _manager = manager;
            _mediator = mediator;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            var newUser = new IdentityUser()
            {
                UserName = user.Login,
                Email = user.Email
            };
            var result = await _manager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                var code = await _manager.GenerateEmailConfirmationTokenAsync(newUser);

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = newUser.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", 
                    $"Please confirm your account by <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > click here </a>.");

                return Ok(newUser);
            }
            else
            {             
                return BadRequest(result.Errors.First().Description);
            }
        }


        public interface IEmailSender
        {
            Task SendEmailAsync(string email, string subject, string htmlMessage);
        }

        public class EmailSender : IEmailSender
        {
            private readonly EmailSettings _emailSettings;

            public EmailSender(IOptions<EmailSettings> emailSettings)
            {
                _emailSettings = emailSettings.Value;
            }

            public Task SendEmailAsync(string email, string subject, string message)
            {
                try
                {
                    // Credentials
                    var credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);

                    // Mail message
                    var mail = new MailMessage()
                    {
                        From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    };

                    mail.To.Add(new MailAddress(email));

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = _emailSettings.MailPort,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = _emailSettings.MailServer,
                        EnableSsl = true,
                        Credentials = credentials
                    };

                    // Send it...         
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    // TODO: handle exception
                    throw new InvalidOperationException(ex.Message);
                }

                return Task.CompletedTask;
            }
        }
    }
}