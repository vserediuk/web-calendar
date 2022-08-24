using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface ICalendarService
    {
        public CalendarViewModel Create(CalendarCreateModel calendar);
        public CalendarViewModel GetById(int id);
        public IEnumerable<CalendarViewModel> GetAllByUserId(int id);
    }
}