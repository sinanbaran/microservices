using Microsoft.AspNetCore.Mvc;

namespace Order.Command.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("");
        }

    }
}
