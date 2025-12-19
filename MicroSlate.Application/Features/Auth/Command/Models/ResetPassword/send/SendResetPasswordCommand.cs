using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.send
{
    public class SendResetPasswordCommand : IRequest<Result<SendResetPasswordResponse>>
    {
        public SendResetPasswordRequest Request { get; set; }

    }
}
