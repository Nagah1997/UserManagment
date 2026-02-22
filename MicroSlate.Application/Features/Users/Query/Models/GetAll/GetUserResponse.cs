
using MicroSlate.Service.DTOs.Users;

namespace MicroSlate.Application.Features.Users.Query.Models.GetAll
{
    public class GetUserResponse
    {
        public List<GetAllUserDTO> Users { get; set; }
    }
}
