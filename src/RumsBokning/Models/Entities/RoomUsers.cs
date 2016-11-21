using System;
using System.Collections.Generic;

namespace RumsBokning.Models.Entities
{
    public partial class RoomUsers
    {
        public int Id { get; set; }
        public int RId { get; set; }
        public string UId { get; set; }

        public virtual Room R { get; set; }
        public virtual Users U { get; set; }
    }
}
