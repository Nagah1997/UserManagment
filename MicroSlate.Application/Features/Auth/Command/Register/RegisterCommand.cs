using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Register
{
    public class RegisterCommand : IRequest<Result<RegisterResponse>>
    {
        public RegisterRequest Request { get; set; }


    }
}
