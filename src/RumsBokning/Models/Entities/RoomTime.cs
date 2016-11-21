using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class RoomTime
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Room IdNavigation { get; set; }
    }
}
