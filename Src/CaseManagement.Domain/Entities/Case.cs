using CaseManagement.Domain.Common;
using CaseManagement.Domain.Enums;
using CaseManagement.Domain.Events;
using CaseManagement.Domain.ValueObjects;

namespace CaseManagement.Domain.Entities;

public class Case : BaseEntity
{
    private readonly List<CaseComment> _comments = new();
    private readonly List<CaseDeadline> _deadlines = new();

    public CaseNumber CaseNumber { get; private set; } = null!;
    public CaseTitle Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public CaseStatus Status { get; private set; }
    public CasePriority Priority { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }
    public DateTime? ClosedAtUtc { get; private set; }
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

    public IReadOnlyCollection<CaseComment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<CaseDeadline> Deadlines => _deadlines.AsReadOnly();

    private Case()
    {
    }

    public Case(
        CaseNumber caseNumber,
        CaseTitle title,
        string description,
        CasePriority priority,
        Guid? categoryId = null)
    {
        if (caseNumber is null)
            throw new ArgumentNullException(nameof(caseNumber));

        if (title is null)
            throw new ArgumentNullException(nameof(title));

        Id = Guid.NewGuid();
        CaseNumber = caseNumber;
        Title = title;
        Description = ValidateDescription(description);
        Priority = priority;
        CategoryId = categoryId;
        Status = CaseStatus.Created;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;

        AddDomainEvent(new CaseCreatedDomainEvent(Id));
    }

    public void AssignTo(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id må ikke være tom.", nameof(userId));

        AssignedUserId = userId;
        Touch();

        AddDomainEvent(new CaseAssignedDomainEvent(Id, userId));
    }

    public void Unassign()
    {
        AssignedUserId = null;
        Touch();
    }

    public void ChangeStatus(CaseStatus newStatus)
    {
        if (Status == CaseStatus.Archived)
            throw new InvalidOperationException("En arkiveret sag kan ikke ændre status.");

        if (Status == CaseStatus.Closed && newStatus != CaseStatus.Archived)
            throw new InvalidOperationException("En lukket sag kan kun arkiveres.");

        Status = newStatus;

        if (newStatus == CaseStatus.Closed)
        {
            ClosedAtUtc = DateTime.UtcNow;
            AddDomainEvent(new CaseClosedDomainEvent(Id));
        }
        else
        {
            ClosedAtUtc = null;
        }

        Touch();
    }

    public void ChangePriority(CasePriority priority)
    {
        Priority = priority;
        Touch();
    }

    public void UpdateDetails(CaseTitle title, string description, Guid? categoryId)
    {
        if (title is null)
            throw new ArgumentNullException(nameof(title));

        Title = title;
        Description = ValidateDescription(description);
        CategoryId = categoryId;
        Touch();
    }

    public void AddComment(Guid authorUserId, string text, bool isInternal)
    {
        var comment = new CaseComment(Id, authorUserId, text, isInternal);
        _comments.Add(comment);
        Touch();

        AddDomainEvent(new CaseCommentAddedDomainEvent(Id, comment.Id));
    }

    public void AddDeadline(string title, DateTime dueDateUtc)
    {
        var deadline = new CaseDeadline(Id, title, dueDateUtc);
        _deadlines.Add(deadline);
        Touch();
    }

    public void Close()
    {
        if (Status == CaseStatus.Archived)
            throw new InvalidOperationException("En arkiveret sag kan ikke lukkes.");

        if (Status == CaseStatus.Closed)
            return;

        Status = CaseStatus.Closed;
        ClosedAtUtc = DateTime.UtcNow;
        Touch();

        AddDomainEvent(new CaseClosedDomainEvent(Id));
    }

    public void Archive()
    {
        if (Status != CaseStatus.Closed)
            throw new InvalidOperationException("Kun lukkede sager kan arkiveres.");

        Status = CaseStatus.Archived;
        Touch();
    }

    private void Touch()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }

    private static string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Beskrivelse er påkrævet.", nameof(description));

        var trimmed = description.Trim();

        if (trimmed.Length > 4000)
            throw new ArgumentException("Beskrivelse må maks være 4000 tegn.", nameof(description));

        return trimmed;
    }
}