using Microsoft.Extensions.DependencyInjection;
using Order.Command.Core.Commands;
using Order.Command.Core.Events;

namespace Order.Command.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection collection)
        {
            collection.AddCommands();
            collection.AddEvents();
            return collection;
        }
    }
}
