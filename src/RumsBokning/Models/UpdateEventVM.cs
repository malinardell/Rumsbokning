﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class UpdateEventVM
    {
        public int BId { get; set; }
        public string UId { get; set; }
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
    }
}
