using System;

namespace DataAccess.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public int NotifyTime { get; set; }
        public int CalendarId { get; set; }
        public int RepeatTypeId { get; set; }
        public string JobId { get; set; }
        public Guid GroupCode { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
    }
}