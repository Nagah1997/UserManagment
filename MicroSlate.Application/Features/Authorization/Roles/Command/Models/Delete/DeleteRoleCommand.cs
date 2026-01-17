using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Authorization.Roles.Command.Models.Delete
{
    public class DeleteRoleCommand : IRequest<Result<DeleteRoleResponse>>
    {
        public string Id { get; set; }
        public DeleteRoleCommand(string id)
        {
            Id = id;
        }

    }
}
