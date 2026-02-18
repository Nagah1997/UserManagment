using Microsoft.AspNetCore.Identity;

namespace MicroSlate.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            string[] roles = new[] { "ADMIN", "USER" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role(role));
                }
            }
        }
    }
}
