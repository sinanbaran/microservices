
using System;
using System.Collections.Generic;
using Order.Command.API.Core.Commands;

namespace Order.Command.API.Domain.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public List<Product> Products { get; set; }

        public class Product
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public string Name { get; set; }
        }
    }


}
