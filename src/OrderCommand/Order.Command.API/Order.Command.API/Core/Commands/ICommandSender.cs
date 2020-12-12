using System;
using System.Threading.Tasks;

namespace Order.Command.API.Core.Commands
{
    public interface ICommandSender
    {
        ICommandSender OnException(Action<System.Exception> onException);
        Task SendAsync<T>(T command) where T : class, ICommand;
        Task<TResult> SendAsync<TResult>(ICommand command);
    }
}