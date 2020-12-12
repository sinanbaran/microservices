using System;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Command.API.Core.Commands;
using Order.Command.API.Core.Domain;
using Order.Command.API.Core.Events;

namespace Order.Command.API
{
    public static class Extensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection collection)
        {
            collection.AddCommands();
            collection.AddEvents();
            return collection;
        }

        public static IServiceCollection AddEventStore(this IServiceCollection collection)
        {

            var configuration = collection.BuildServiceProvider().GetService<IConfiguration>();
            var eventStoreConnection = configuration.GetSection("EventStoreConnection").Get<string>();

            var settings = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .Build();

            var connection = EventStoreConnection.Create(settings, new Uri(eventStoreConnection));
            connection.ConnectAsync().GetAwaiter().GetResult();
            collection.AddSingleton(connection);

            collection.AddScoped<AggregateRepository>();

            return collection;
        }
    }
}
