using System;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Command.Core.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection builder)
        {
            builder.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            builder.AddSingleton<ICommandSender, CommandSender>();

            return builder;
        }
    }
}