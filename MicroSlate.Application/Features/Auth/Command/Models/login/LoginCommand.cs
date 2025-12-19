using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Models.login
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public LoginRequest Request { get; set; }

    }

}

