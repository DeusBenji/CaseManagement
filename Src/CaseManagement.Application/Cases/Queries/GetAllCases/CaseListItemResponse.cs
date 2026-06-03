using CaseManagement.Domain.Enums;

namespace CaseManagement.Application.Cases.Queries.GetAllCases
{
    public sealed record CaseListItemResponse(
    Guid Id,
    string CaseNumber,
    string Title,
    string Description,
    CaseStatus Status,
    CasePriority Priority,
    Guid? CategoryId,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
}
