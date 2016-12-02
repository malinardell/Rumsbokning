using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class EditRoomVM
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int? Capacity { get; set; }
        public bool? HasWhiteBoard { get; set; }
        public bool? HasProjector { get; set; }
        public bool? HasTvScreen { get; set; }
    }
}
