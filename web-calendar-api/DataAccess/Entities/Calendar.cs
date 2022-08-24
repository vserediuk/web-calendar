using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Calendar : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int OwnerUserId { get; set; }
        public ICollection<User> InvitedUsers { get; set; }
    }
}