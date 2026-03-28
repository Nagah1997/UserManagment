

using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Features.Users.Query.Models.GetAll;
using MicroSlate.Application.Features.Users.Query.Models.GetOneUser;
using MicroSlate.Service.Abstracts.Users;

namespace MicroSlate.Application.Features.Users.Query.Handler
{
    public class UserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
        , IRequestHandler<GetOneUserQuery, Result<GetOneUserResponse>>
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
        public async Task<Result<GetOneUserResponse>> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
        {

            var users = await _usersService.GetUser(request.Request.Id);
            return Result.Success(new GetOneUserResponse { Users = users });


        }

    }
}