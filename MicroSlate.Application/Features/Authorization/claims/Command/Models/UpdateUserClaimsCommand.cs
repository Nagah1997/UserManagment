using Ardalis.Result;
using MediatR;
using MicroSlate.Service.DTOs.Authorization;

namespace MicroSlate.Application.Features.Authorization.claims.Command.Models
{
    public class UpdateUserClaimsCommand : IRequest<Result<UpdateUserClaimsResponse>>
    {
        public UpdateUserClaimsRequest DTO { get; set; }
    }
}
