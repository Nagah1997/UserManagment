using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Authorization.Roles.Command.Models.Edit
{
    public class EditRoleCommand : IRequest<Result<EditRoleResponse>>
    {
        public EditRoleCommand(EditRoleRequest editRoleRequest)
        {
            EditRoleRequest = editRoleRequest;

        }



        public EditRoleRequest EditRoleRequest { get; set; }
    }
}
