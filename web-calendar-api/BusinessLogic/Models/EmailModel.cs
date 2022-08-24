using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class EmailModel
    {
        public IEnumerable<UserViewModel> DestinationUsers { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentPath { get; set; }
    }
}