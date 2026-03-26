namespace CaseManagement.Domain.Common
{
    public class BaseDomainEvent
    {
        public DateTime OccuredOnUtc { get; } = DateTime.UtcNow;
    }
}
