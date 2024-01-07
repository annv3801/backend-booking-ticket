using System.Drawing;
using Application.Common.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using QRCoder;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailName").Value));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
    
        var builder = new BodyBuilder();
        builder.TextBody = body;
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
