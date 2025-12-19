using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroSlate.Infrastructure.Identity
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "mahmoud2fci57@gmail.com",
                    FullName = "MicroSlate",

                    PhoneNumber = "Mrn@gah57",

                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "Mrn@gah57");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
