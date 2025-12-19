using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Models.Register
{
    public class RegisterCommand : IRequest<Result<RegisterResponse>>
    {
        public RegisterRequest Request { get; set; }


    }
}
