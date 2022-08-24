using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ReceiveEmailNotifications { get; set; }
        public ICollection<Calendar> Calendars { get; set; }
        public ICollection<Calendar> SharedCalendars { get; set; }
    }
}