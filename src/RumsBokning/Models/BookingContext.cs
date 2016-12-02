using Microsoft.AspNetCore.Http;
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

        public Room GetRoomTitleVM(int id)
        {
            return Room.SingleOrDefault(r => r.Id == id);
        }

        public ShowBookingVM[] GetBooking(string id)
        {
            var bookings = Room
                 .Join(RoomTime.Where(c => c.UId == id && c.EndTime > DateTime.Now),
                 r => r.Id,
                 rt => rt.RId,
                 (r, rt) => new ShowBookingVM
                 {
                     Id = rt.Id,
                     RoomName = r.Name,
                     Start = rt.StartTime,
                     End = rt.EndTime,
                     Description = rt.Description
                 });

            return bookings.ToArray(); ;
        }

        public int CancelBooking(int id)
        {
            var bookingToDelete = RoomTime.SingleOrDefault(b => b.Id == id);

            RoomTime.Remove(bookingToDelete);
            return SaveChanges();
        }

        public async Task<string> GetUserCategory(string userName)
        {
            var tempUser = await userManager.FindByNameAsync(userName);
            var tempUser2 = this.Users.SingleOrDefault(x => x.Id == tempUser.Id);
            var userCategory = tempUser2.Category;

            return userCategory;
        }

        public async Task<UserSettingVM> GetUserInfo(string userName)
        {
            var tempUser = await userManager.FindByNameAsync(userName);
            var tempUser2 = this.Users.SingleOrDefault(x => x.Id == tempUser.Id);

            var currentUser = new UserSettingVM
            {
                UserName = tempUser2.Email,
                FirstName = tempUser2.FirstName,
                LastName = tempUser2.LastName,
                Category = tempUser2.Category
            };

            return currentUser;
        }

        public void UpdateUser(UserSettingVM viewModel)
        {
            var tempUser = Users.SingleOrDefault(b => b.Email == viewModel.UserName);

            tempUser.Email = viewModel.UserName;
            tempUser.FirstName = viewModel.FirstName;
            tempUser.LastName = viewModel.LastName;
            tempUser.Category = viewModel.Category;

            SaveChanges();
        }

        public async Task<AdminSettingVM> GetAdminInfo(string userName)
        {
            var tempUser = await userManager.FindByNameAsync(userName);
            var tempUser2 = this.Users.SingleOrDefault(x => x.Id == tempUser.Id);

            var currentUser = new AdminSettingVM
            {
                UserName = tempUser2.Email,
                FirstName = tempUser2.FirstName,
                LastName = tempUser2.LastName,
                Category = tempUser2.Category
            };

            return currentUser;
        }

        public void UpdateAdmin(AdminSettingVM viewModel)
        {
            var tempUser = Users.SingleOrDefault(b => b.Email == viewModel.UserName);

            tempUser.Email = viewModel.UserName;
            tempUser.FirstName = viewModel.FirstName;
            tempUser.LastName = viewModel.LastName;
            tempUser.Category = viewModel.Category;

            SaveChanges();
        }
        public int DeleteRoom(int id)
        {
            var roomToDelete = Room.SingleOrDefault(b => b.Id == id);

            foreach (var booking in RoomTime.Where(rt => rt.RId == id))
            {
                RoomTime.Remove(booking);
            }

            Room.Remove(roomToDelete);
            return SaveChanges();
        }

        public UpdateRoomVM GetRoomToUpdate(int id)
        {
            var roomToUpdate = Room.SingleOrDefault(b => b.Id == id);

            var tempRoom = new UpdateRoomVM
            {
               RoomName = roomToUpdate.Name,
               Capacity = roomToUpdate.Capacity,
               HasProjector = roomToUpdate.HasProjector,
               HasTvScreen = roomToUpdate.HasTvScreen,
               HasWhiteBoard = roomToUpdate.HasWhiteBoard
            };
            return tempRoom;
        }

        public void UpdateRoom(UpdateRoomVM viewModel)
        {
            var tempRoom = Room.SingleOrDefault(b => b.Id == viewModel.Id);

            tempRoom.Name = viewModel.RoomName;
            tempRoom.Capacity = viewModel.Capacity;
            tempRoom.HasWhiteBoard = viewModel.HasWhiteBoard;
            tempRoom.HasTvScreen = viewModel.HasTvScreen;
            tempRoom.HasProjector = viewModel.HasProjector;

            SaveChanges();
        }

        public EditRoomVM[] ShowRooms()
        {
            return Room
                .Select(c => new EditRoomVM
                {
                    Id = c.Id,
                    RoomName = c.Name,
                    Capacity = c.Capacity,
                    HasProjector = c.HasProjector,
                    HasTvScreen = c.HasTvScreen,
                    HasWhiteBoard = c.HasWhiteBoard
                }).ToArray();
        }

        public async Task CreateAdmin(CreateAdminVM viewModel)
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

        public DeleteUserVM[] GetUsers()
        {
            return Users
                .Select(c => new DeleteUserVM
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Category = c.Category,
                    Email = c.Email
                }).ToArray();
        }

        public int DeleteUser(string id)
        {
            var userToDelete = Users.Include(p => p.RoomTime).SingleOrDefault(b => b.Id == id);

            if (userToDelete != null)
            {
                foreach (var booking in userToDelete.RoomTime)
                {
                    RoomTime.Remove(booking);
                }

                Users.Remove(userToDelete);
                return SaveChanges();
            }
            else
                return 0;
        }
    }
}
