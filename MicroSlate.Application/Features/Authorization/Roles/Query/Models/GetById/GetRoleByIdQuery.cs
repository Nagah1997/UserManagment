using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Authorization.Roles.Query.Models.GetById
{
    public class GetRoleByIdQuery : IRequest<Result<GetRoleByIdResponse>>
    {
        public string Id { get; set; }
    }
}
