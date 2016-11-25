using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class RoomTime
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int RId { get; set; }
        public string UId { get; set; }

        public virtual Room R { get; set; }
        public virtual Users U { get; set; }
    }
}
