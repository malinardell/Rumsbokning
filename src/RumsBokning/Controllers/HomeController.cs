using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RumsBokning.Models;
using RumsBokning.Models.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RumsBokning.Controllers
{
    public class HomeController : Controller
    {
        BookingContext context;
        public HomeController(BookingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Home(int id)
        {
            var viewModel = context.GetRoomVM(id);
            return Json(viewModel);
        }
    }
}
