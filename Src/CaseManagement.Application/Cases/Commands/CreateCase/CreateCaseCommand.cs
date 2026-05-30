using CaseManagement.Domain.Enums;

namespace CaseManagement.Application.Cases.Commands.CreateCase
{
    public sealed record CreateCaseCommand(
    string CaseNumber,
    string Title,
    string Description,
    CasePriority Priority,
    Guid? CategoryId = null
);
}
