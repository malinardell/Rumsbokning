using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class Room
    {
        public Room()
        {
            RoomTime = new HashSet<RoomTime>();
            RoomUsers = new HashSet<RoomUsers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public bool? HasWhiteBoard { get; set; }
        public bool? HasProjector { get; set; }
        public bool? HasTvScreen { get; set; }

        public virtual ICollection<RoomTime> RoomTime { get; set; }
        public virtual ICollection<RoomUsers> RoomUsers { get; set; }
    }
}
