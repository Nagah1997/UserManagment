
using Ardalis.Result;
using MediatR;
using MicroSlate.Service.Abstracts.Auth;

namespace MicroSlate.Application.Features.Auth.Command.login
{
    public class loginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IAuthService _auth;
        public loginHandler(IAuthService auth)
        {
            _auth = auth;

        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var token = await _auth.Login(request.Request.Email, request.Request.Password);


                return Result<LoginResponse>.Success(new LoginResponse() { Token = token });
            }
            catch (Exception ex)
            {


                return Result<LoginResponse>.Error($"{ex.Message}");

            }




        }
    }


}
