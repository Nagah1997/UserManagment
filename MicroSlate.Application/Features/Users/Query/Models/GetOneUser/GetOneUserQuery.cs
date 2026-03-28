using Ardalis.Result;
using MediatR;


namespace MicroSlate.Application.Features.Users.Query.Models.GetOneUser
{
    public class GetOneUserQuery : IRequest<Result<GetOneUserResponse>>
    {

        public GetOneUserRequest Request { get; set; }

        public GetOneUserQuery(GetOneUserRequest request)
        {
            Request = request;
        }

    }
}
