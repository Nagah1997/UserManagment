namespace MicroSlate.Infrastructure.Identity
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser user, IList<string> roles);



    }
}
