using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEventService
    {
        public void Create(EventCreateModel calendarEvent, string userEmail);
        public EventViewModel Edit(EventEditModel calendarEvent, string userEmail);
        public void Delete(int id, string userEmail);
        public EventViewModel GetById(int id);
        public IEnumerable<EventViewModel> GetAllByCalendarId(int id);
    }
}