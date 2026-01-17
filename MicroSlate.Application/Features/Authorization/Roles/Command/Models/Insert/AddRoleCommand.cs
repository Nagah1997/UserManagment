using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Authorization.Roles.Command.Models.Insert
{
    public class AddRoleCommand : IRequest<Result<AddRoleResponse>>
    {
        public AddRoleCommand(AddRoleRequest addRoleRequest)
        {

            AddRoleRequest = addRoleRequest;

        }
        public AddRoleRequest AddRoleRequest { get; set; }

    }
}
