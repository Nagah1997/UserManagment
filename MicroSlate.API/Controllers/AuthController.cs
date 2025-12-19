using MediatR;
using MicroSlate.Application.Features.Auth.Command.Models.login;
using MicroSlate.Application.Features.Auth.Command.Models.Register;
using MicroSlate.Application.Features.Auth.Query.Models.ConfirmEmail;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = MicroSlate.Application.Features.Auth.Command.Models.login.LoginRequest;
using RegisterRequest = MicroSlate.Application.Features.Auth.Command.Models.Register.RegisterRequest;

namespace MicroSlate.API.Controllers
{
    public class AuthController : BaseController
    {
        private IMediator _mediator;




        public AuthController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest dto)
        {
            var token = await _mediator.Send(new RegisterCommand() { Request = dto });
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest dto)
        {
            var token = await _mediator.Send(new LoginCommand() { Request = dto });
            return Ok(new { token });
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest dto)
        {
            var token = await _mediator.Send(new ConfirmEmailQuery() { Request = dto });
            return Ok(new { token });
        }




    }

}
