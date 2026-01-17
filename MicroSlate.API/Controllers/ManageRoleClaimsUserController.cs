using MediatR;
using MicroSlate.Application.Features.Authorization.claims.Command.Models;
using MicroSlate.Application.Features.Authorization.claims.Query.Models.UserRolesData;
using MicroSlate.Service.DTOs.Authorization;
using MicroSlate.Service.Implementions.Authorization.claims;
using Microsoft.AspNetCore.Mvc;

namespace MicroSlate.API.Controllers
{
    public class ManageRoleClaimsUserController : BaseController
    {
        private IMediator _mediator;




        public ManageRoleClaimsUserController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpPost("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesRequest dto)
        {
            var token = await _mediator.Send(new UpdateUserRolesCommand() { request = dto });
            return Ok(new { token });
        }
        [HttpPost("UpdateUserClaims")]
        public async Task<IActionResult> UpdateUserClaims(UpdateUserClaimsRequest dto)
        {
            var token = await _mediator.Send(new UpdateUserClaimsCommand() { DTO = dto });
            return Ok(new { token });
        }
        [HttpGet("{UserId}")]
        public async Task<IActionResult> UserRolesData(string UserId)
        {
            var token = await _mediator.Send(new UserRolesDataQuery() { UserId = UserId });
            return Ok(new { token });
        }
        [HttpGet("ManageUserClaims/{UserId}")]
        public async Task<IActionResult> ManageUserClaims(string UserId)
        {
            var token = await _mediator.Send(new ManageUserClaimsQuery() { UserId = UserId });
            return Ok(new { token });
        }
    }
}
