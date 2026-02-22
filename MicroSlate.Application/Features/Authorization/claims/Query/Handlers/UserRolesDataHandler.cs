using Ardalis.Result;
using AutoMapper;
using MediatR;
using MicroSlate.Application.Features.Authorization.claims.Query.Models.UserRolesData;
using MicroSlate.Application.Resources;
using MicroSlate.Infrastructure.Identity;
using MicroSlate.Service.Abstracts.Authorization.claims;
using MicroSlate.Service.Implementions.Authorization.claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using ManageUserRolesDTO = MicroSlate.Service.Implementions.Authorization.claims.ManageUserRolesDTO;


namespace MicroSlate.Application.Features.Authorization.claims.Query.Handlers
{
    public class UserRolesDataHandler :
        IRequestHandler<UserRolesDataQuery, Result<UserRolesDataResponse>>,
          IRequestHandler<ManageUserClaimsQuery, Result<ManageUserClaimsResponse>>

    {
        #region MyRegion
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IClaimService _claimsService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion
        #region MyRegion
        public UserRolesDataHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<SharedResources> stringLocalizer, IClaimService authorizationService, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _claimsService = authorizationService;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion
        #region MyRegion
        public async Task<Result<UserRolesDataResponse>> Handle(UserRolesDataQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            var roles = await _claimsService.ManageUserRolesData(user);
         //   var result = _mapper.Map<List<ManageUserRolesDTO>>(roles);
            return Result.Success(new UserRolesDataResponse { Result = roles });
        }
        public async Task<Result<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return Result.Error(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var result = await _claimsService.ManageUserClaimData(user);
            return Result.Success(new ManageUserClaimsResponse { Result = result });
        }

        #endregion

    }

}

