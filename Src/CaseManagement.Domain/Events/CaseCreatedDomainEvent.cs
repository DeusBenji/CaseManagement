using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Events;

public sealed class CaseCreatedDomainEvent : BaseDomainEvent
{
    public Guid CaseId { get; }

    public CaseCreatedDomainEvent(Guid caseId)
    {
        CaseId = caseId;
    }
}