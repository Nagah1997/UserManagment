using Ardalis.Result;
using MediatR;
using MicroSlate.Service.DTOs.Authorization;

namespace MicroSlate.Application.Features.Authorization.claims.Query.Models.UserRolesData
{
    public class ManageUserClaimsQuery : IRequest<Result<ManageUserClaimsResult>>
    {
        public string UserId { get; set; }
    }

}

