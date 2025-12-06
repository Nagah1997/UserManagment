using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace MicroSlate.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
    }
}
