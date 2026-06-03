namespace CaseManagement.Application.Cases.Dtos
{
    public sealed record PagedResponse<T>(IReadOnlyList<T> Items, int PageNumber, int PageSize, int TotalCount, int TotalPages);
}

