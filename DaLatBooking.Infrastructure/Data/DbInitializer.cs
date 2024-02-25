using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Application.Common.Utility;
using DaLatBooking.Domain.Entities;
using DaLatBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WhiteLagoon.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }

                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
                    _userManager.CreateAsync(new User
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        Name = "Admin Thung Lũng Mây",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        PhoneNumber = "0904064751",
                    }, "Pa$$W0rd").GetAwaiter().GetResult();

                    User user = _context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}