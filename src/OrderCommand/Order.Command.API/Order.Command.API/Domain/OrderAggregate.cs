using System;
using System.Collections.Generic;
using Order.Command.API.Core;
using Order.Command.API.Core.Domain;
using Order.Command.API.Domain.DomainEvents;

namespace Order.Command.API.Domain
{
    public class OrderAggregate : Aggregate
    {
        public string OrderNumber { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public string Owner { get; protected set; }
        public List<OrderItem> OrderItems { get; protected set; } = new List<OrderItem>();
        public string State { get; protected set; }
        public OrderAggregate Create(Guid id, string owner)
        {
            if (string.IsNullOrWhiteSpace(owner))
                throw new Exception("owner can not be null");

            Apply(new OrderCreated()
            {
                AggregateId = id,
                CreatedOn = DateTime.Now,
                OrderNumber = new Random().Next(0, 80000).ToString().PadLeft(10, '0'),
                Owner = owner,
                State = "WaitingToPay"
            });
            return this;
        }
        public OrderAggregate AddOrderItem(OrderItem item)
        {
            Apply(new OrderItemAdded()
            {
                AggregateId = Id,
                Name = item.Name,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
            return this;
        }
        public OrderAggregate OrderCompleted()
        {
            Apply(new PaymentCompleted()
            {
                AggregateId = Id,
            });
            return this;
        }

        #region When
        public void When(PaymentCompleted @event)
        {
            State = "PaymentCompleted";
        }

        public void When(OrderItemAdded @event)
        {
            OrderItems.Add(new OrderItem()
            {
                Name = @event.Name,
                Quantity = @event.Quantity,
                UnitPrice = @event.UnitPrice,
                ProductId = @event.ProductId
            });
        }
        public void When(OrderCreated @event)
        {
            Id = @event.AggregateId;
            CreatedOn = @event.CreatedOn;
            OrderNumber = @event.OrderNumber;
            Owner = @event.Owner;
            State = @event.State;
        }
        #endregion


    }
}
