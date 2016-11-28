using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class CreateRoomVM
    {
        [Display(Name = "Rumsnamn")]
        [Required(ErrorMessage = "Fyll i rumsnamn")]
        public string RoomName { get; set; }
        [Display(Name = "Antal platser")]
        public int Capacity { get; set; }
        [Display(Name = "Whiteboard")]
        public bool HasWhiteBoard { get; set; }
        [Display(Name = "Projektor")]
        public bool HasProjector { get; set; }
        [Display(Name = "Tv-skärm")]
        public bool HasTvScreen { get; set; }
    }
}
