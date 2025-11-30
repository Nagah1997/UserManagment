using Microsoft.AspNetCore.Identity;

namespace MicroSlate.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }


    }
}
