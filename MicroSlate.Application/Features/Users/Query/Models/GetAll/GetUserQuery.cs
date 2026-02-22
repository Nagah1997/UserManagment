using Ardalis.Result;
using MediatR;


namespace MicroSlate.Application.Features.Users.Query.Models.GetAll
{
    public class GetUserQuery: IRequest<Result<GetUserResponse>>
    {
    }
}
