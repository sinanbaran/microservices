using System;
using Order.Command.API.Core.Commands;

namespace Order.Command.API.Domain.Commands
{
    public class PaymentCompletedCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
