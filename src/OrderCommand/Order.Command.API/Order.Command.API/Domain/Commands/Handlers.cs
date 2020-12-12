using System.Collections.Generic;
using System.Threading.Tasks;
using Order.Command.API.Core.Commands;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.Commands
{
    public class Handlers : ICommandHandler<CreateOrderCommand>
    {
        private readonly AggregateRepository _aggregateRepository;

        public Handlers(AggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }
        public async Task<CommandResponse> HandleAsync(CreateOrderCommand command)
        {

            var order = new OrderAggregate()
                .Create(command.Id, command.Owner);

            foreach (var item in command.Products)
            {
                order.AddOrderItem(new OrderItem()
                {
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Name = item.Name,
                    ProductId = item.ProductId
                });
            }

            await _aggregateRepository.SaveAsync(order);

            return new CommandResponse()
            {
                Result = order.OrderNumber,
                Events = (IList<Core.Events.IEvent>)order.GetChanges()
            };
        }
    }
}
