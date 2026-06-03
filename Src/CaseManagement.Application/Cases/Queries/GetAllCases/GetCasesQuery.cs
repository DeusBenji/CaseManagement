namespace CaseManagement.Application.Cases.Queries.GetAllCases
{
    public sealed record GetCasesQuery(
    int PageNumber = 1,
    int PageSize = 20);
}
