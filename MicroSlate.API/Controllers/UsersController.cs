using MediatR;
using MicroSlate.Application.Features.Users.Query.Models.GetAll;
using MicroSlate.Application.Features.Users.Query.Models.GetOneUser;
using Microsoft.AspNetCore.Mvc;

namespace MicroSlate.API.Controllers
{
    public class UsersController : BaseController
    {
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet("GetUsers")]
        public async Task<GetUserResponse> get([FromQuery] GetUserRequest request)
        {
            var response = await _mediator.Send(new GetUserQuery(request));
            return response;
        }
        [HttpGet("GetUser")]
        public async Task<GetOneUserResponse> GetUser([FromQuery] GetOneUserRequest request)
        {
            var response = await _mediator.Send(new GetOneUserQuery(request));
            return response;
        }

    }
}
