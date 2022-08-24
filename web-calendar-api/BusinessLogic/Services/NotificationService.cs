using BusinessLogic.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Linq;
using Hangfire;
using System;

namespace BusinessLogic.Services
{
    public class NotificationService : Interfaces.INotificationService
    {
        private readonly SmtpSettings _smtpSettings;

        public NotificationService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmailSmtp(EmailModel model)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.Username));
            message.To.AddRange(model.DestinationUsers.Where(u => u.ReceiveEmailNotifications).Select(u => MailboxAddress.Parse(u.Email)));
            message.Subject = model.Subject;
            var emailBody = new BodyBuilder { HtmlBody = model.Body };
            
            if (model.AttachmentPath != null)
                emailBody.Attachments.Add(model.AttachmentPath);
            
            message.Body = emailBody.ToMessageBody();

            using SmtpClient client = new SmtpClient();
            client.Connect(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSSL);
            client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
            client.Send(message);
            client.Disconnect(true);
        }

        public void SendEmail(EmailModel model) => BackgroundJob.Enqueue(() => SendEmailSmtp(model));

        public string ScheduleEmail(EmailModel model, DateTime dateTime) =>
            BackgroundJob.Schedule(
                () => SendEmailSmtp(model), dateTime
            );

        public void CancelEmail(string jobId) => BackgroundJob.Delete(jobId);
    }
}