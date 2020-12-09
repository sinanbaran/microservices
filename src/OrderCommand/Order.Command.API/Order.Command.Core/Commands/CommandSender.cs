using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Order.Command.Core.Events;

namespace Order.Command.Core.Commands
{
    internal sealed class CommandSender : ICommandSender
    {
        private readonly IServiceScopeFactory _serviceFactory;

        private readonly IMapper _mapper;
        private readonly IEventSender _eventSender;
        private readonly ILogger<CommandSender> _logger;
        public CommandSender(
            IServiceScopeFactory serviceFactory,
            IMapper mapper,
            IEventSender eventSender,
            ILogger<CommandSender> logger)
        {
            _serviceFactory = serviceFactory;
            _mapper = mapper;
            _eventSender = eventSender;
            _logger = logger;
        }

        private Action<Exception> _onException;
        private async Task<CommandResponse> HandlerAsync<T>(T command)
           where T : class, ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            CommandResponse response = default;
            try
            {
                using var scope = _serviceFactory.CreateScope();
                var handler = scope.ServiceProvider.GetService<ICommandHandler<T>>();
                response = await handler.HandleAsync(command);
                foreach (var @event in response.Events)
                {
                    dynamic concreteObject = _mapper.Map(@event, @event.GetType(), @event.GetType());

                    _eventSender.PublishAsync(concreteObject);
                }
            }
            catch (Exception ex)
            {
                if (_onException == null)
                    throw;
                _onException(ex);
            }
            return response;
        }
        public ICommandSender OnException(Action<System.Exception> onException)
        {
            _onException = onException;
            return this;
        }
        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            await HandlerAsync(command);
        }
        public async Task<TResult> SendAsync<TResult>(ICommand command)
        {
            dynamic concreteObject = _mapper.Map(command, command.GetType(), command.GetType());
            var response = await HandlerAsync(concreteObject);
            return response?.Result != null ? (TResult)response.Result : default;
        }
    }
}