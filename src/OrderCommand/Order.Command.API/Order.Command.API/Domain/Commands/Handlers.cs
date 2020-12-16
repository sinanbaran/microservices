using System;
using System.Threading.Tasks;
using Order.Command.API.Core.Commands;
using Order.Command.API.Core.Domain;

namespace Order.Command.API.Domain.Commands
{
    public class Handlers :
        ICommandHandler<CreateOrderCommand>,
        ICommandHandler<PaymentCompletedCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public Handlers(IAggregateRepository aggregateRepository)
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
                Events = order.GetChanges()
            };
        }

        public async Task<CommandResponse> HandleAsync(PaymentCompletedCommand command)
        {
            var order = await _aggregateRepository.LoadAsync<OrderAggregate>(command.Id);

            if (order == null)
                throw new Exception("Order not found");


            order.OrderCompleted();


            await _aggregateRepository.SaveAsync(order);

            return new CommandResponse()
            {
                Events = order.GetChanges()
            };
        }
    }
}
