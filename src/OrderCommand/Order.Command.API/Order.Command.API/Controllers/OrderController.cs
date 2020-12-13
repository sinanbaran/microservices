using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Order.Command.API.Core.Commands;
using Order.Command.API.Domain.Commands;

namespace Order.Command.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IConfiguration _configuration;

        public OrderController(ICommandSender commandSender, IConfiguration configuration)
        {
            _commandSender = commandSender;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            return Ok(await _commandSender.SendAsync<string>(command));
        }

        [HttpGet("test")]
        public Task<string> Test()
        {
            return Task.FromResult("test");
        }


        [HttpPatch("complete")]
        public Task Create([FromBody] PaymentCompletedCommand command)
            => _commandSender.SendAsync<string>(command);
    }
}