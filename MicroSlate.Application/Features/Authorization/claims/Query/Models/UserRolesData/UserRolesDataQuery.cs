using Ardalis.Result;
using MediatR;

namespace MicroSlate.Service.Implementions.Authorization.claims
{
    public class UserRolesDataQuery : IRequest<Result<UserRolesDataResponse>>
    {
        public string UserId { get; set; }
    }
}
