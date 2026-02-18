using Microsoft.AspNetCore.Identity;

namespace MicroSlate.Infrastructure.Identity
{
    public class Role : IdentityRole
    {
        public Role() : base() { } // Default constructor

        public Role(string roleName) : base(roleName) { } // Constructor ياخد الاسم
    }
}
