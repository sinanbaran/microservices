using System;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Command.Core.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection builder)
        {
            builder.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            builder.AddSingleton<IEventSender, EventSender>();

            return builder;
        }

    }
}