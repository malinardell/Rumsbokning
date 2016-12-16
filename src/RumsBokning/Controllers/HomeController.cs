using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RumsBokning.Models;
using RumsBokning.Models.Entities;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RumsBokning.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        BookingContext context;
        UserManager<IdentityUser> userManager;
        public HomeController(BookingContext context, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
       

        [HttpPost]
        public async Task <bool> CreateEvent(CreateEventVM viewModel)
        {
            await context.BookRoom(viewModel, User.Identity.Name);
            return true;
        }

        [HttpPost]
        public string GetRoomTitle(int id)
        {
            return JsonConvert.SerializeObject(context.GetRoomTitleVM(id));
        }

        [HttpGet]
        public IActionResult ShowBooking()
        {
            var currentUserId = userManager.GetUserId(HttpContext.User);

            var viewModel = context.GetBooking(currentUserId);
            return View(viewModel);
        }

        [HttpPost]
        public string ShowBooking(int id)
        {
            return context.CancelBooking(id).ToString();
        }

        [HttpGet]
        public async Task <IActionResult> UserSetting()
        {
            var viewModel = await context.GetUserInfo(User.Identity.Name);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UserSetting(UserSettingVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                context.UpdateUser(viewModel);
                return RedirectToAction(nameof(HomeController.UserSetting));
            }
        }

        [HttpPost]
        public async Task<string> HideAdmin()
        { 
            return await context.GetUserCategory(User.Identity.Name);
        }

        [HttpPost]
        public async Task<string> HideSettings()
        {
            return await context.GetUserCategory(User.Identity.Name);
        }

        [HttpPost]
        public int ChangeBooking(UpdateEventVM viewModel)
        {
         return context.UpdateBooking(viewModel);

        }

    }
}


