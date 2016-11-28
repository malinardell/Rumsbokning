using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RumsBokning.Models;
using Microsoft.AspNetCore.Authorization;
using RumsBokning.Models.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RumsBokning.Controllers
{
    public class AccountController : Controller
    {
        BookingContext context;
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            BookingContext context
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(IndexVM viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Skapa DB-schemat
            //await identityContext.Database.EnsureCreatedAsync();

            //string userName = "malin_ardell@hotmail.com";
            //string password = "Hejhopp123";
            // Create user
            //var result = await userManager.CreateAsync(new IdentityUser(viewModel.UserName),viewModel.Password);
            
            var result = await signInManager.PasswordSignInAsync(
                viewModel.UserName, viewModel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(IndexVM.UserName),
                    "Incorrect login credentials");
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                //Response.Cookies.Append("Users.Id", "viewModel.Id");

                return RedirectToAction(nameof(HomeController.Home), "home");
            }
            else
                return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task <IActionResult> CreateUser(CreateUserVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await context.AddUser(viewModel);
                return RedirectToAction(nameof(HomeController.Home), "home");
            }
            else
                return View(viewModel);

        }

    }
}
