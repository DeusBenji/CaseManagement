namespace CaseManagement.Domain.Common
{
    public abstract class BaseEntity
    {

        public readonly List<BaseDomainEvent> DomainEvents = new List<BaseDomainEvent>();
        public Guid Id { get; protected set; }
        public IReadOnlyCollection<BaseDomainEvent> GetDomainEvents() => DomainEvents.AsReadOnly();
        protected void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            DomainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            DomainEvents.Clear();
        }

    }
}
