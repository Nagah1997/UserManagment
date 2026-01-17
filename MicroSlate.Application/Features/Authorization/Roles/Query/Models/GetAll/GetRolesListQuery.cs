using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetAll
{
    public class GetRolesListQuery : IRequest<Result<GetRolesListResponse>>
    {
    }
}
