using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.DTOs.Authorization;
using MicroSlate.Service.Implementions.Authorization.claims;

namespace MicroSlate.Service.Abstracts.Authorization.claims
{
    public interface IClaimService
    {
        Task<ManageUserRolesDTO> ManageUserRolesData(ApplicationUser user);
        Task<string> UpdateUserRoles(UpdateUserRolesRequest request);

        Task<ManageUserClaimsResult> ManageUserClaimData(ApplicationUser user);

        Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);


    }
}
