using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.confirm;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.Reset;
using MicroSlate.Application.Features.Auth.Command.Models.ResetPassword.send;
using MicroSlate.Service.Abstracts.Auth;

namespace MicroSlate.Application.Features.Auth.Command.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<SendResetPasswordCommand, Result<SendResetPasswordResponse>>,
        IRequestHandler<ConfirmResetPasswordCommand, Result<confirmResetPasswordResponse>>,
         IRequestHandler<ResetPasswordCommand, Result<ResetPasswordResponse>>
    {
        private readonly IAuthService _auth;
        public ResetPasswordHandler(IAuthService auth)
        {
            _auth = auth;

        }
        public async Task<Result<SendResetPasswordResponse>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {



            var message = await _auth.SendResetPasswordCode(request.Request.Email);


            return Result<SendResetPasswordResponse>.Success(new SendResetPasswordResponse() { message = message });







        }

        public async Task<Result<confirmResetPasswordResponse>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var message = await _auth.ConfirmResetPassword(request.Request.Code, request.Request.Email);


            return Result<confirmResetPasswordResponse>.Success(new confirmResetPasswordResponse() { message = message });

        }

        public async Task<Result<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var message = await _auth.ResetPassword(request.Request.Email, request.Request.Password);


            return Result<ResetPasswordResponse>.Success(new ResetPasswordResponse() { message = message });
        }
    }

}

