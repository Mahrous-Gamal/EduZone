using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace EduZone.Models
{
    public class SendEmailDegree
    {

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }
        public SendEmailDegree(string calback)
        {
            Email = "EduZone_Mai1@outlook.com";
            DisplayName = "EduZone Mail System";
            Password = "EduZoneMail1";
            Host = "smtp-mail.outlook.com";
            Port = 587;
            Subject = "System Mail";
            Body = "<h1>Click down link To Show Your Exam</h1>" + $"<a href= \"{calback}\" > Click Here</a>";
        }
        public async Task SendEmailAsync(string mailTo, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(Email),
                Subject = Subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            if (attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = Body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(DisplayName, Email));

            var smtp = new SmtpClient();

            smtp.Connect(Host, Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(Email, Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }

    }
}