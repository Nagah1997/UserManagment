using Ardalis.Result;
using MediatR;

namespace MicroSlate.Application.Features.Auth.Query.Models.ConfirmEmail
{
    public class ConfirmEmailQuery : IRequest<Result<ConfirmEmailResponse>>
    {

        public ConfirmEmailRequest Request { get; set; }



    }
}
