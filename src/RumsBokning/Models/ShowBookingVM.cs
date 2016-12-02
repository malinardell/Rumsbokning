using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models.Entities
{
    public class ShowBookingVM
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Description { get; set; }
    }
}
