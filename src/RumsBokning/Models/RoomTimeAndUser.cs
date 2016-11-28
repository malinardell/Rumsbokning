using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class RoomTimeAndUser
    {
        public int RId { get; set; }
        public string UId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
