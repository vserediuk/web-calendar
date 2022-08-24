using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class EventEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public int NotifyTime { get; set; }
        public EventRepeatType RepeatType { get; set; }
        public IEnumerable<UserViewModel> InvitedUsers { get; set; }
        public IFormFile AttachedFile { get; set; }
        public int? FileId { get; set; }
    }
}
