using CaseManagement.Domain.Entities;

namespace CaseManagement.Application.Abstraction.Persistence
{
    public interface ICaseRepository
    {
        Task<Case?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        Task<Case?> GetByCaseNumberAsync(string caseNumber, CancellationToken cancellationToken = default);
        Task<bool> ExistsByCaseNumberAsync (string caseNumber, CancellationToken cancellationToken = default);
        Task AddAsync(Case caseEntity, CancellationToken cancellationToken = default);
    }
}
