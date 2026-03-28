using MicroSlate.Service.DTOs.Users;

namespace MicroSlate.Service.Abstracts.Users
{
    public interface IUsersService
    {
        public Task<List<GetAllUserDTO>> GetAllUsers();
        public Task<GetAllUserDTO> GetUser(Guid Id);
    }
}
