using MicroSlate.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSlate.Service.Abstracts.Users
{
    public interface IUsersService
    {
        public Task<List<GetAllUserDTO>> GetAllUsers();  
    }
}
