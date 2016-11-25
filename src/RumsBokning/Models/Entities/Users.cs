using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            RoomTime = new HashSet<RoomTime>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }

        public virtual ICollection<RoomTime> RoomTime { get; set; }
    }
}
