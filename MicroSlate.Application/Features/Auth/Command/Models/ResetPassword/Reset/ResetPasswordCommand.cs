using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.Reset
{
    public class ResetPasswordCommand : IRequest<Result<ResetPasswordResponse>>
    {
        public ResetPasswordRequest Request { get; set; }

    }
}
