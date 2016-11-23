using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            RoomUsers = new HashSet<RoomUsers>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }

        public virtual ICollection<RoomUsers> RoomUsers { get; set; }
    }
}
