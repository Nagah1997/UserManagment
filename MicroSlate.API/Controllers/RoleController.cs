using MediatR;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Delete;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Edit;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Insert;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetAll;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetById;
using Microsoft.AspNetCore.Mvc;

namespace MicroSlate.API.Controllers
{
    public class RoleController : BaseController
    {
        private IMediator _mediator;
        public RoleController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet()]
        public async Task<GetRolesListResponse> get([FromForm] GetRolesListQuery command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpGet("{id}")]
        public async Task<GetRoleByIdResponse> Create(string id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery() { Id = id });
            return response;
        }
        [HttpPost()]
        public async Task<AddRoleResponse> Create([FromForm] AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpPut()]
        public async Task<EditRoleResponse> Edit([FromForm] EditRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpDelete("{id}")]
        public async Task<DeleteRoleResponse> Delete([FromRoute] string id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(id));
            return response;
        }

    }


}



