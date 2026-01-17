using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Features.Authorization.claims.Command.Models;
using MicroSlate.Application.Resources;
using MicroSlate.Service.Abstracts.Authorization.claims;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace MicroSlate.Application.Features.Authorization.claims.Command.Handlers
{
    public class ClaimsCommandHandler :
        IRequestHandler<UpdateUserClaimsCommand, Result<UpdateUserClaimsResponse>>,
        IRequestHandler<UpdateUserRolesCommand, Result<UpdateUserRolesResponse>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IClaimService _claimsService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                    IClaimService claimsService)
        {
            _claimsService = claimsService;

            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions




        public async Task<Result<UpdateUserClaimsResponse>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _claimsService.UpdateUserClaims(request.DTO);
            switch (result)
            {
                case "UserIsNull": return Result.Error(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldClaims": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToUpdateClaims]);
            }

            return Result.Success(new UpdateUserClaimsResponse() { }, _stringLocalizer[SharedResourcesKeys.Success]);
        }
        public async Task<Result<UpdateUserRolesResponse>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _claimsService.UpdateUserRoles(request.request);
            switch (result)
            {
                case "UserIsNull": return Result.Error(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return Result.Error(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }

            return Result.Success(new UpdateUserRolesResponse() { }, _stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}

#endregion




