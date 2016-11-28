using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                        EndTime = rt.EndTime

                    })
                    .Where(x => x.RId == id /*&& x.EndTime >= DateTime.Now*/)
                    .ToList();

                foreach (var item in newRoomVM.RoomTimeAndUser)
                {
                    events.Add(new EventObject(item.FirstName + " " + item.LastName, item.StartTime, item.EndTime));
                }
            }
            return events;
        }

        public Room[] GetAllRooms()
        {
            return Room
             .Select(c => new Room
             {
                 Capacity = c.Capacity,
                 Name = c.Name,
                 Id = c.Id
             })
              .ToArray();
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
