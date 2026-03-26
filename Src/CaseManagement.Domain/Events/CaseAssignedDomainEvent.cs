using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Events;

public sealed class CaseAssignedDomainEvent : BaseDomainEvent
{
    public Guid CaseId { get; }
    public Guid AssignedUserId { get; }

    public CaseAssignedDomainEvent(Guid caseId, Guid assignedUserId)
    {
        CaseId = caseId;
        AssignedUserId = assignedUserId;
    }
}