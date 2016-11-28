using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models.Entities
{
    public class RoomVM
    {

        public string RoomName { get; set; }
        public int? Capacity { get; set; }
        public bool? HasWhiteBoard { get; set; }
        public bool? HasProjector { get; set; }
        public bool? HasTvScreen { get; set; }
        public List<RoomTimeAndUser> RoomTimeAndUser { get; set; }
        public string Description { get; set; }
    }
}
