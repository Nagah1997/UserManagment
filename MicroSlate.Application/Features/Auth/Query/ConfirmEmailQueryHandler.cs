using Ardalis.Result;

using MediatR;
using MicroSlate.Application.Features.Auth.Query.ConfirmEmail;
using MicroSlate.Application.Resources;
using MicroSlate.Service.Abstracts.Auth;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace MicroSlate.Application.Features.Auth.Query
{
    public class ConfirmEmailQueryHandler : IRequestHandler<ConfirmEmailQuery, Result<ConfirmEmailResponse>>
    {
        private readonly IAuthService _auth;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        public ConfirmEmailQueryHandler(IAuthService auth, IStringLocalizer<SharedResources> sharedResources)
        {
            _sharedResources = sharedResources;
            _auth = auth;

        }
        public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _auth.ConfirmEmail(request.Request.UserId, request.Request.Code);
            if (confirmEmail == "ErrorWhenConfirmEmail")

                return Result<ConfirmEmailResponse>.Error(_sharedResources[SharedResourcesKeys.ErrorWhenConfirmEmail]);
            return Result<ConfirmEmailResponse>.Success(new ConfirmEmailResponse() { confirm = _sharedResources[SharedResourcesKeys.ConfirmEmailDone] });
        }


    }

}

