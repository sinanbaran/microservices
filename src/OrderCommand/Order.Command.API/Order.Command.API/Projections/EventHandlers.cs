using System.Threading.Tasks;
using MongoDB.Driver;
using Order.Command.API.Core.Events;
using Order.Command.API.Domain.DomainEvents;
using Order.Command.API.Projections.MongoDb;

namespace Order.Command.API.Projections
{
    public class EventHandlers :
        IEventHandler<OrderCreated>,
        IEventHandler<OrderItemAdded>,
        IEventHandler<PaymentStateChanged>
    {
        private readonly IMongoDbContext _dbContext;

        public EventHandlers(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(OrderCreated @event)
        {

            _dbContext.OrderMaterializedView().InsertOne(new OrderMaterializedView()
            {
                OrderNumber = @event.OrderNumber,
                Owner = @event.Owner,
                CreatedOn = @event.CreatedOn,
                Id = @event.AggregateId,
            });

            //   var item = _dbContext.OrderMaterializedView().Find(x => x.Id == @event.AggregateId).FirstOrDefault();
            return Task.CompletedTask;
        }

        public Task HandleAsync(OrderItemAdded @event)
        {
            // var item = _dbContext.OrderMaterializedView().Find(x => x.Id == @event.AggregateId).FirstOrDefault();

            var filter = Builders<OrderMaterializedView>.Filter.Eq(x => x.Id, @event.AggregateId);

            var update = Builders<OrderMaterializedView>.Update.Push(s => s.Items, new OrderMaterializedView.OrderItemMaterialized()
            {
                Name = @event.Name,
                Quantity = @event.Quantity,
                UnitPrice = @event.UnitPrice,
                ProductId = @event.ProductId,
                TotalPrice = @event.UnitPrice * @event.Quantity
            });

            _dbContext.OrderMaterializedView().FindOneAndUpdate(filter, update);

            return Task.CompletedTask;
        }

        public Task HandleAsync(PaymentStateChanged @event)
        {
            //var item = _dbContext.OrderMaterializedView().Find(x => x.Id == @event.AggregateId).FirstOrDefault();


            var filter = Builders<OrderMaterializedView>.Filter.Eq(x => x.Id, @event.AggregateId);

            var update = Builders<OrderMaterializedView>.Update.Set(s => s.State, @event.State);

            _dbContext.OrderMaterializedView().FindOneAndUpdate(filter, update);

            return Task.CompletedTask;

        }
    }
}
