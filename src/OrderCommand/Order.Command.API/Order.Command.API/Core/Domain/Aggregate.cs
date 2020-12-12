using System;
using System.Collections.Generic;
using System.Linq;
using Order.Command.API.Core.Events;
using ReflectionMagic;

namespace Order.Command.API.Core.Domain
{
    public abstract class Aggregate
    {

        readonly IList<DomainEvent> _changes = new List<DomainEvent>();

        public Guid Id { get; protected set; } = Guid.Empty;
        public long Version { get; private set; } = -1;

        public void Apply(DomainEvent @event)
        {
            this.AsDynamic().When(@event);
            _changes.Add(@event);
        }
        public void Load(long version, IEnumerable<DomainEvent> history)
        {
            Version = version;

            foreach (var e in history)
            {
                this.AsDynamic().When(e);
            }
        }
        public IList<DomainEvent> GetChanges() => _changes.ToArray();
    }
}
