using BusinessLogic.Models;
using System;

namespace BusinessLogic.Services.Interfaces
{
    public interface INotificationService
    {
        public void SendEmail(EmailModel model);
        public string ScheduleEmail(EmailModel model, DateTime dateTime);
        public void CancelEmail(string jobId);
    }
}