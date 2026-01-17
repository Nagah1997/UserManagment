using Ardalis.Result;
using MediatR;
using MicroSlate.Service.Implementions.Authorization.claims;

namespace MicroSlate.Application.Features.Authorization.claims.Command.Models
{
    public class UpdateUserRolesCommand : IRequest<Result<UpdateUserRolesResponse>>
    {
        public UpdateUserRolesRequest request { get; set; }
    }
}
