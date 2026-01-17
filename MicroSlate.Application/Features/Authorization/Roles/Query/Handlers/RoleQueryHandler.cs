using Ardalis.Result;
using AutoMapper;
using MediatR;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetAll;
using MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetById;
using MicroSlate.Application.Resources;
using MicroSlate.Service.Abstracts.Authorization.Roles;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace MicroSlate.Application.Features.Authorization.Roles.Query.Handlers
{
    public class RoleQueryHandler :
        IRequestHandler<GetRoleByIdQuery, Result<GetRoleByIdResponse>>,
        IRequestHandler<GetRolesListQuery, Result<GetRolesListResponse>>




    {
        #region MyRegion
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;

        #endregion
        #region MyRegion
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IRolesService authorizationService, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _rolesService = authorizationService;
            _mapper = mapper;
        }

        #endregion
        #region MyRegion
        public async Task<Result<GetRolesListResponse>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _rolesService.GetRolesList();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Result.Success(new GetRolesListResponse { Result = result });
        }

        public async Task<Result<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _rolesService.GetRoleById(request.Id);
            if (role == null) return Result.NotFound(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Result.Success(new GetRoleByIdResponse { Result = result });
        }


        #endregion

    }
}
