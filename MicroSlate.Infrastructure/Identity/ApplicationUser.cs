using Microsoft.AspNetCore.Identity;

namespace MicroSlate.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }

        public string Code { get; set; } = string.Empty;


    }
}
