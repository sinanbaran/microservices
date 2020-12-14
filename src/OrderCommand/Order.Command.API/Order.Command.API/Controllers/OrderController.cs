using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using Order.Command.API.Core.Commands;
using Order.Command.API.Domain.Commands;
using Order.Command.API.Projections;
using Order.Command.API.Projections.MongoDb;

namespace Order.Command.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IConfiguration _configuration;

        public OrderController(ICommandSender commandSender, IConfiguration configuration, IMongoDbContext dbContext)
        {
            _commandSender = commandSender;
            _configuration = configuration;




            
            //var filter = Builders<OrderMaterializedView>.Filter.Eq("_id", Guid.Parse("e75845fd-1113-4dfc-ad85-9dcf5410ba97"));

            //var update = Builders<OrderMaterializedView>.Update.Set(s => s.OrderNumber, "007James");

            //dbContext.OrderMaterializedView().FindOneAndUpdate(filter, update);


            //var item = dbContext.OrderMaterializedView().Find(_ => _.Id == Guid.Parse("e75845fd-1113-4dfc-ad85-9dcf5410ba97")).ToList();
            
            ////var items = dbContext.OrderMaterializedView().Find(_ => true).ToList();


        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            return Ok(await _commandSender.SendAsync<string>(command));
        }

        [HttpGet("test")]
        public Task<string> Test()
        {
            return Task.FromResult(_configuration.GetSection("ServiceName").Get<string>());
        }


        [HttpPatch("complete")]
        public Task Create([FromBody] PaymentCompletedCommand command)
            => _commandSender.SendAsync<string>(command);
    }
}