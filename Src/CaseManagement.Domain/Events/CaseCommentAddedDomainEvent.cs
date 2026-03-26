using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Events;

public sealed class CaseCommentAddedDomainEvent : BaseDomainEvent
{
    public Guid CaseId { get; }
    public Guid CommentId { get; }

    public CaseCommentAddedDomainEvent(Guid caseId, Guid commentId)
    {
        CaseId = caseId;
        CommentId = commentId;
    }
}