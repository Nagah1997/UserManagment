using MediatR;

namespace MicroSlate.API.Controllers
{
    public class EmailController : BaseController
    {
        private IMediator _mediator;

        public EmailController(IMediator mediator)
        {

            _mediator = mediator;

        }


    }
}
