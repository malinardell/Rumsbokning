using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RumsBokning.Models;
using RumsBokning.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class UpdateRoomVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i rumsnamn")]
        [Display(Name = "Rumsnamn")]
        public string RoomName { get; set; }
        [Display(Name = "Antal platser")]
        public int? Capacity { get; set; }
        [Display(Name = "Whiteboard")]
        public bool? HasWhiteBoard { get; set; }
        [Display(Name = "Projektor")]
        public bool? HasProjector { get; set; }
        [Display(Name = "Tv-skärm")]
        public bool? HasTvScreen { get; set; }

    }
}
