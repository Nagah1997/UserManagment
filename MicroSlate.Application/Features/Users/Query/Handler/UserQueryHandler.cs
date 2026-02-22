

using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetById;
using MicroSlate.Application.Features.Users.Query.Models.GetAll;
using MicroSlate.Service.Abstracts.Users;

namespace MicroSlate.Application.Features.Users.Query.Handler
{
    public class UserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
    {
        private readonly IUsersService _usersService;
        public UserQueryHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {

            var users = await _usersService.GetAllUsers();
            return Result.Success(new GetUserResponse { Users = users });


        }

    }
}