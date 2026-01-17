using MicroSlate.Application.Features.Authorization.Roles.Query;
using MicroSlate.Infrastructure.Identity;

namespace MicroSlate.Application.Mapping.Roles
{
    public partial class MappingProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<Role, GetRolesListResult>();
        }
    }
}
