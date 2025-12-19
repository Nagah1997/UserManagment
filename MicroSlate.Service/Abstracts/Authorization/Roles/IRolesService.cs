using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.DTOs.Authorization;
namespace MicroSlate.Service.Abstracts.Authorization.Roles
{
    public interface IRolesService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleDTO request);
        public Task<string> DeleteRoleAsync(string roleId);
        public Task<bool> IsRoleExistById(string roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(string id);
    }
}
