using CaseManagement.Domain.Common;

namespace CaseManagement.Domain.Entities;

public class CaseDeadline : BaseEntity
{
    public Guid CaseId { get; private set; }
    public string Title { get; private set; } = null!;
    public DateTime DueDateUtc { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    private CaseDeadline(){}

    internal CaseDeadline(Guid caseId, string title, DateTime dueDateUtc)
    {
        if (caseId == Guid.Empty)
            throw new ArgumentException("Case id må ikke være tom.", nameof(caseId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Deadline-titel er påkrævet.", nameof(title));

        var trimmed = title.Trim();

        if (trimmed.Length > 200)
            throw new ArgumentException("Deadline-titel må maks være 200 tegn.", nameof(title));

        Id = Guid.NewGuid();
        CaseId = caseId;
        Title = trimmed;
        DueDateUtc = dueDateUtc;
        IsCompleted = false;
    }

    public void Complete()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;
        CompletedAtUtc = DateTime.UtcNow;
    }
}