using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Order.Command.API.Projections
{
    public class OrderMaterializedView
    {
        [BsonId]
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Owner { get; set; }
        public List<OrderItemMaterialized> Items { get; set; } = new List<OrderItemMaterialized>();
        public string State { get; set; }
        public class OrderItemMaterialized
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public double TotalPrice { get; set; }
        }
    }
}
