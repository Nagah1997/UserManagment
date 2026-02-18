using AutoMapper;

namespace MicroSlate.Application.Mapping.Roles
{
    using AutoMapper;

    public partial  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            GetRolesListMapping();
            GetRoleByIdMapping();
        }
    }

}