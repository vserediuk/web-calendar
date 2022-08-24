using System;

namespace DataAccess.Entities
{
    public class Reminder : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int CalendarId { get; set; }
    }
}