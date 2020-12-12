using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Command.API.Core.Commands;
using Order.Command.API.Core.Domain;
using Order.Command.API.Domain;
using Order.Command.API.Domain.Commands;

namespace Order.Command.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        public OrderController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            return Ok(await _commandSender.SendAsync<string>(command));

        }
    }
}