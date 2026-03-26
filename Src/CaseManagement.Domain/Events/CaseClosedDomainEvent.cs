using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Events;

public sealed class CaseClosedDomainEvent : BaseDomainEvent
{
    public Guid CaseId { get; }

    public CaseClosedDomainEvent(Guid caseId)
    {
        CaseId = caseId;
    }
}