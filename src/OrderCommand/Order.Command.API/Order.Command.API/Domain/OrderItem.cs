using System;

namespace Order.Command.API.Domain
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
