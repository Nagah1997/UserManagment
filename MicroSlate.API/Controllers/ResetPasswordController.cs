using MediatR;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.confirm;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.Reset;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.send;
using Microsoft.AspNetCore.Mvc;

namespace MicroSlate.API.Controllers
{
    public class ResetPasswordController : BaseController
    {
        private IMediator _mediator;

        public ResetPasswordController(IMediator mediator)
        {

            _mediator = mediator;

        }

        [HttpPost("sentPassword")]
        public async Task<IActionResult> sentPasswordRequest(SendResetPasswordRequest dto)
        {
            var token = await _mediator.Send(new SendResetPasswordCommand() { Request = dto });
            return Ok(new { token });
        }

        [HttpPost("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword(ConfirmResetPasswordRequest dto)
        {
            var token = await _mediator.Send(new ConfirmResetPasswordCommand() { Request = dto });
            return Ok(new { token });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest dto)
        {
            var token = await _mediator.Send(new ResetPasswordCommand() { Request = dto });
            return Ok(new { token });
        }
    }

}
