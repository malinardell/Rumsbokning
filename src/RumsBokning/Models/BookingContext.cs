using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models.Entities
{
    public partial class BookingContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
           
        }

        public async Task AddUser(CreateUserVM viewModel, UserManager<IdentityUser> userManager)
        {
            var tempUser = new IdentityUser(viewModel.UserName);
            var result = await userManager.CreateAsync(tempUser, viewModel.Password);

            if (result.Succeeded)
            {
                var newUser = new Users
                {
                    Id=tempUser.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.UserName,
                    Category = viewModel.Category
                };

                this.Users.Add(newUser);
                await this.SaveChangesAsync();
            }

            
        }



    }
}
