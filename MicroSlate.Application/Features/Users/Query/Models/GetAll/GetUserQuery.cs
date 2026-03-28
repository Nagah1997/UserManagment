using Ardalis.Result;
using MediatR;


namespace MicroSlate.Application.Features.Users.Query.Models.GetAll
{
    public class GetUserQuery : IRequest<Result<GetUserResponse>>
    {
        public GetUserRequest Request { get; set; }
        public GetUserQuery(GetUserRequest request)
        {
            Request = request;
        }
    }
}
