using System.Threading.Tasks;

namespace Order.Command.API.Core.Events
{
    public interface IEventSender
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}