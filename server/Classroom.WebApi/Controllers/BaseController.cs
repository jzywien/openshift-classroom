using Microsoft.AspNetCore.Mvc;

namespace Classroom.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
