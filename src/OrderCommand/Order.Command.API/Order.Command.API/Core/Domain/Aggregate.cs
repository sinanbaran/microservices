using System;
using System.Collections.Generic;
using ReflectionMagic;

namespace Order.Command.API.Core.Domain
{
    public abstract class Aggregate
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        public Guid Id { get; protected set; } = Guid.Empty;
        public long LastCommittedVersion { get; private set; } = -1;
        public void Apply(DomainEvent @event)
        {
            this.AsDynamic().When(@event);
            _changes.Add(@event);
        }
        public void Load(long version, IEnumerable<IDomainEvent> history)
        {
            LastCommittedVersion = version;
            foreach (var e in history)
                this.AsDynamic().When(e);
        }
        public IReadOnlyCollection<IDomainEvent> GetChanges() => _changes;
    }
}
