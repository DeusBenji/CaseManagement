using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Application.Common.Exceptions;


namespace CaseManagement.Application.Cases.Queries.GetCaseById
{
    public class GetCaseByIdQueryHandler
    {
        private readonly ICaseRepository _caseRepository;

        public GetCaseByIdQueryHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }


        public async Task<CaseResponse> Handle(GetCaseByIdQuery querry, CancellationToken cancellationToken = default)
        {
            var caseEntity = await _caseRepository.GetByIdAsync(querry.Id);
            if (caseEntity == null)
                throw new NotFoundException("Sagen blev ikke fundet.");

            var caseResposne = new CaseResponse(
            caseEntity.Id,
            caseEntity.CaseNumber.Value,
            caseEntity.Title.Value,
            caseEntity.Description,
            caseEntity.Status,
            caseEntity.Priority,
            caseEntity.CategoryId,
            caseEntity.CreatedAtUtc,
            caseEntity.UpdatedAtUtc
        );
            return caseResposne;
        }
    }
}
