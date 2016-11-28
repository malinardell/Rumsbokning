using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RumsBokning.Models;
using RumsBokning.Models.Entities;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            return View(context.GetAllRooms());
        }

        [HttpGet]
        public IActionResult GetCalendar(int id)
        {
            var viewModel = context.GetRoomVM(id);
            return Json(viewModel);
        }
        [HttpGet]
        public IActionResult CreateRoom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRoom(CreateRoomVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                context.AddRoom(viewModel);
                return RedirectToAction(nameof(HomeController.CreateRoom));
            }
        }
        [HttpGet]
        public IActionResult UpdateRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task <bool> CreateEvent(CreateEventVM viewModel)
        {
            await context.BookRoom(viewModel, User.Identity.Name);
            return true;
        }
    }
}


