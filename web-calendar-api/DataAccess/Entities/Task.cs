using System;

namespace DataAccess.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
        public int CalendarId { get; set; }
    }
}