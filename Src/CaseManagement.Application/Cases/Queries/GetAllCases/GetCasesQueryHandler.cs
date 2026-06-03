using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Application.Common.Exceptions;
using CaseManagement.Application.Cases.Dtos;

namespace CaseManagement.Application.Cases.Queries.GetAllCases
{
    public sealed class GetCasesQueryHandler
    {
        private readonly ICaseRepository _caseRepository;

        public GetCasesQueryHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }


        public async Task<PagedResponse<CaseListItemResponse>> Handle(GetCasesQuery query, CancellationToken cancellationToken = default)
        {

            if (query.PageNumber < 1) 
                throw new RequestValidationException("PageNumber skal mindst være 1.");

            if (query.PageSize < 1 || query.PageSize > 100)
                throw new RequestValidationException("PageSize skal være mellem 1 og 100.");

            var totalCount = await _caseRepository.CountAsync(cancellationToken);

            var cases = await _caseRepository.GetPagedAsync(pageNumber: query.PageNumber, pageSize: query.PageSize, cancellationToken: cancellationToken);

            var items = cases.Select(c => new CaseListItemResponse(
                c.Id,
                c.CaseNumber.Value,
                c.Title.Value,
                c.Description,
                c.Status,
                c.Priority,
                c.CategoryId,
                c.CreatedAtUtc,
                c.UpdatedAtUtc
            )).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

            return new PagedResponse<CaseListItemResponse>(
                items,
                query.PageNumber,
                query.PageSize,
                totalCount,
                totalPages);


        }
    }
}