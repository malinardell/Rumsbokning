using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RumsBokning.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models.Entities
{
    public partial class BookingContext
    {

        UserManager<IdentityUser> userManager;
        public BookingContext(DbContextOptions<BookingContext> options, UserManager<IdentityUser> userManager) : base(options)
        {
            this.userManager = userManager;
        }

        public async Task AddUser(CreateUserVM viewModel)
        {
            
                var tempUser = new IdentityUser(viewModel.UserName);
                var result = await userManager.CreateAsync(tempUser, viewModel.Password);

                if (result.Succeeded)
                {
                    var newUser = new Users
                    {
                        Id = tempUser.Id,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = viewModel.UserName,
                        Category = viewModel.Category
                    };

                    this.Users.Add(newUser);
                    await this.SaveChangesAsync();
                }
            
            
            
        }
        public async Task BookRoom(CreateEventVM viewModel, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var userId = user.Id;
            var newRoomTime = new RoomTime
            {
                RId = viewModel.RoomId,
                UId = userId,
                //LastName = viewModel.LastName,
                //FirstName = viewModel.FirstName,
                StartTime = viewModel.Start,
                EndTime = viewModel.End,
                Description = viewModel.Description
            };
            this.RoomTime.Add(newRoomTime);
            await this.SaveChangesAsync();
        }

        public List<EventObject> GetRoomVM(int id)
        {
            var events = new List<EventObject>();
            var newRoomVM = new RoomVM();

            /* Hitta rummet med parameter id i tabellen Room i db */
            var room = Room.SingleOrDefault(r => r.Id == id);

            if (room != null)
            {
                newRoomVM.RoomName = room.Name;
                newRoomVM.Capacity = room.Capacity;
                newRoomVM.HasProjector = room.HasProjector;
                newRoomVM.HasTvScreen = room.HasTvScreen;
                newRoomVM.HasWhiteBoard = room.HasWhiteBoard;
                

                newRoomVM.RoomTimeAndUser = RoomTime
                    .Join(
                    Users,
                    rt => rt.UId,
                    u => u.Id,
                    (rt, u) => new RoomTimeAndUser
                    {
                        RId = rt.RId,
                        UId = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        StartTime = rt.StartTime,
                        EndTime = rt.EndTime,
                        Description = rt.Description
                    })
                    .Where(x => x.RId == id /*&& x.EndTime >= DateTime.Now*/)
                    .ToList();

                foreach (var item in newRoomVM.RoomTimeAndUser)
                {
                    events.Add(new EventObject(item.FirstName + " " + item.LastName, item.StartTime, item.EndTime, item.Description));
                }
            }
            return events;
        }

        public HomeVM GetAllRooms()
        {
            return new HomeVM
            {
                RoomItems =
                Room.Select(c => new SelectListItem
                {
                    Text = $"{c.Name} ({c.Capacity})",
                    Value = c.Id.ToString()
                })
              .ToArray()
            };
            
        }

        public void AddRoom(CreateRoomVM viewModel)
        {
            var newRoom = new Room
            {
                Name = viewModel.RoomName,
                Capacity = viewModel.Capacity,
                HasProjector = viewModel.HasProjector,
                HasTvScreen = viewModel.HasTvScreen,
                HasWhiteBoard = viewModel.HasWhiteBoard
            };

            this.Room.Add(newRoom);
            this.SaveChangesAsync();
        }

    }
}
