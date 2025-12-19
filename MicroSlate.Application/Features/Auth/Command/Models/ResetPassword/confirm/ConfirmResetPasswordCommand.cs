using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.confirm
{
    public class ConfirmResetPasswordCommand : IRequest<Result<confirmResetPasswordResponse>>
    {
        public ConfirmResetPasswordRequest Request { get; set; }


    }
}
