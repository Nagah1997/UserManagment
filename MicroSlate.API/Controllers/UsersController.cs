using MediatR;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetAll;
using MicroSlate.Application.Features.Users.Query.Models.GetAll;
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
        public async Task<GetUserResponse> get([FromQuery] GetUserQuery command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
    
    }
}
