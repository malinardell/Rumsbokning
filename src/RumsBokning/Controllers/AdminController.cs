using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RumsBokning.Models;
using RumsBokning.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RumsBokning.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        BookingContext context;
        UserManager<IdentityUser> userManager;
        public AdminController(BookingContext context, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
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
                return RedirectToAction(nameof(AdminController.CreateRoom));
            }
        }

        [HttpPost]
        public string DeleteRoom(int id)
        {
            return context.DeleteRoom(id).ToString();
        }

        [HttpGet]
        public IActionResult EditRoom()
        {
            return View(context.ShowRooms());
        }

        [HttpGet]
        public IActionResult UpdateRoom(int id)
        {
            return View(context.GetRoomToUpdate(id));
        }

        [HttpPost]
        public IActionResult UpdateRoom(UpdateRoomVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                context.UpdateRoom(viewModel);
                return RedirectToAction(nameof(AdminController.EditRoom));
            }
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateAdmin(CreateAdminVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await context.CreateAdmin(viewModel);
                return RedirectToAction(nameof(AdminController.CreateAdmin));
            }
            else
                return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AdminSetting()
        {
            var viewModel = await context.GetAdminInfo(User.Identity.Name);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AdminSetting(AdminSettingVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                context.UpdateAdmin(viewModel);
                return RedirectToAction(nameof(AdminController.AdminSetting));
            }
        }

        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View(context.GetUsers());
        }

        [HttpPost]
        public string DeleteUser(string id)
        {
            return context.DeleteUser(id).ToString();
        }

    }

}
