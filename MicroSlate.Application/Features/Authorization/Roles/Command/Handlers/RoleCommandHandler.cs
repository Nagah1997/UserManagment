using Ardalis.Result;
using MediatR;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Delete;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Edit;
using MicroSlate.Application.Features.Authorization.Roles.Command.Models.Insert;
using MicroSlate.Application.Resources;
using MicroSlate.Service.Abstracts.Authorization.Roles;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace MicroSlate.Application.Features.Authorization.Roles.Command.Handlers
{
    public class RoleCommandHandler :
        IRequestHandler<AddRoleCommand, Result<AddRoleResponse>>,
        IRequestHandler<EditRoleCommand, Result<EditRoleResponse>>,
        IRequestHandler<DeleteRoleCommand, Result<DeleteRoleResponse>>


    {
        #region MyRegion
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IRolesService _rolesService;

        #endregion
        #region MyRegion
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IRolesService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _rolesService = authorizationService;
        }

        #endregion
        #region MyRegion
        public async Task<Result<AddRoleResponse>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _rolesService.AddRoleAsync(request.AddRoleRequest.RoleName);
            if (result == "Success") return Result.Success(new AddRoleResponse(), _stringLocalizer[SharedResourcesKeys.Success]);
            return Result.Error(_stringLocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Result<EditRoleResponse>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _rolesService.EditRoleAsync(request.EditRoleRequest.DTO);
            if (result == "notFound") return Result.Error(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "Success") return Result.Success(new EditRoleResponse(), _stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return Result.Error(result.ToLower());
        }

        public async Task<Result<DeleteRoleResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _rolesService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return Result.Error(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "Used") return Result.Error(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "Success") return Result.Success(new DeleteRoleResponse(), _stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return Result.Error(result.ToLower());
        }

        #endregion

    }
}
