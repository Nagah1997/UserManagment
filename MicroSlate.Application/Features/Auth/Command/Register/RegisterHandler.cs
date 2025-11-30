using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Resources;
using MicroSlate.Service.Abstracts.Auth;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace MicroSlate.Application.Features.Auth.Command.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        private readonly IAuthService _auth;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        public RegisterHandler(IAuthService auth)
        {
            _auth = auth;

        }
        public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _auth.Register(request.Request.RegisterDTO);
                switch (createResult)
                {
                    case "EmailIsExist": return Result<RegisterResponse>.Error(_sharedResources[SharedResourcesKeys.EmailIsExist]);
                    case "UserNameIsExist": return Result<RegisterResponse>.Error(_sharedResources[SharedResourcesKeys.UserNameIsExist]);
                    case "ErrorInCreateUser": return Result<RegisterResponse>.Error(_sharedResources[SharedResourcesKeys.FaildToAddUser]);
                    case "Failed": return Result<RegisterResponse>.Error(_sharedResources[SharedResourcesKeys.TryToRegisterAgain]);
                    case "Success": return Result<RegisterResponse>.Success(new RegisterResponse() { Token = createResult });
                    default: return Result<RegisterResponse>.Error(createResult);
                }

            }
            catch (Exception ex)
            {

                return Result<RegisterResponse>.Error($"{ex.Message}");

            }


        }

    }
}
