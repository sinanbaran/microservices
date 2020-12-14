using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Order.Command.API.Core.Domain
{
    public class AggregateRepository
    {
        private readonly IEventStoreConnection _eventStore;

        public AggregateRepository(IEventStoreConnection eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveAsync<T>(T aggregate) where T : Aggregate, new()
        {

            var events = aggregate.GetChanges()
                .Select(@event => new EventData(Guid.NewGuid(), @event.GetType().Name, true,
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, _serializerSettings)),
                    Encoding.UTF8.GetBytes(@event.GetType().FullName!)))
                .ToArray();

            if (!events.Any())
            {
                return;
            }
            var streamName = GetStreamName(aggregate, aggregate.Id);

            await _eventStore.AppendToStreamAsync(streamName, aggregate.LastCommittedVersion, events);
        }
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.None
        };
        public async Task<T> LoadAsync<T>(Guid aggregateId) where T : Aggregate, new()
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentException("aggregateId can not be null or empty");

            var aggregate = new T();
            var streamName = GetStreamName(aggregate, aggregateId);

            var nextPageStart = 0L;
            do
            {
                var page = await _eventStore.ReadStreamEventsForwardAsync(
                    streamName, nextPageStart, 4096, false);

                if (page.Events.Length > 0)
                {
                    var @events = page.Events.Select(@event =>
                         (IDomainEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(@event.OriginalEvent.Data),
                              Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata))!)
                       ).ToArray();

                    aggregate.Load(
                        page.Events.Last().Event.EventNumber, @events);
                }

                nextPageStart = !page.IsEndOfStream ? page.NextEventNumber : -1;
            } while (nextPageStart != -1);


            return aggregate.Id == Guid.Empty ? null : aggregate;
        }

        private string GetStreamName<T>(T type, Guid aggregateId) => $"{type.GetType().Name}-{aggregateId}";
    }
}

