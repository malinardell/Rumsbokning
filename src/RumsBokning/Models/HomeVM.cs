using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class HomeVM
    {
        public SelectListItem[] RoomItems { get; set; }
    }
}
