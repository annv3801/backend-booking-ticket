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

    public void SendEmail(string to, string subject, string body, bool isBooking)
    {
        if (!isBooking)
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

        if (isBooking)
        {
            // Generate QR code image
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeDetail = qrCodeGenerator.CreateQrCode(body, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeDetail);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
        
            // Convert the QR code image a byte array
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] qrCodeBytes = ms.ToArray();
        
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
        
            var builder = new BodyBuilder();
            var test = @"<html>
                        <body>
                            <h1 style='text-center font-weight:bold'>Cảm ơn bạn đã đặt vé thành công</h1>
                            <h2>Vui lòng đưa mã QR cho nhân viên tại quầy</h2>
                            <img src=""cid:qr_code"" alt=""QR Code"">
                        </body>
                    </html>";
            builder.HtmlBody = test;
            var image = builder.LinkedResources.Add("qr_code.png", qrCodeBytes, ContentType.Parse("image/png"));
            image.ContentId = "qr_code";
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        
    }
}
