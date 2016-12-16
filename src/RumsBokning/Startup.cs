using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RumsBokning.Models.Entities;

namespace RumsBokning
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = @"Server=tcp:identitybooking.database.windows.net,1433;Database=IdentityBookingDB;User ID=bookingadmin@identitybooking;Password=Waam1234;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            //var connString = @"Server=tcp:identitybooking.database.windows.net,1433;Initial Catalog=IdentityBookingDB;Persist Security Info=False;User ID=bookingadmin;Password=Waam1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //var connString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Identitybooking;Integrated Security=True";

            services.AddDbContext<BookingContext>(o =>
              o.UseSqlServer(connString));

            services.AddDbContext<IdentityDbContext>(o =>
                o.UseSqlServer(connString));

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                o.Password.RequiredLength = 8;
                o.Password.RequireDigit = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Cookies.ApplicationCookie.LoginPath = "/account/index";
            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSession();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            {
                app.UseSession();
                app.UseDeveloperExceptionPage();
                app.UseIdentity();
                app.UseStaticFiles();
                //app.UseMvcWithDefaultRoute();

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Account}/{action=Index}/{id?}");
                });
            }
        }
    }
}
