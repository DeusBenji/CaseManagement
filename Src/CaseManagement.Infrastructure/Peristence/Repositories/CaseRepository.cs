using CaseManagement.Application.Abstraction.Persistence;
using CaseManagement.Domain.Common;
using CaseManagement.Domain.Entities;
using CaseManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Infrastructure.Peristence.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Case?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cases
                .Include(c => c.Comments)
                .Include(c => c.Deadlines)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Case?> GetByCaseNumberAsync(string caseNumber, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cases
                .Include(c => c.Comments)
                .Include(c => c.Deadlines)
                .FirstOrDefaultAsync(c => c.CaseNumber.Value == caseNumber, cancellationToken);
        }

        public async Task<bool> ExistsByCaseNumberAsync(string caseNumber, CancellationToken cancellationToken = default)
        {
            var valueObject = new CaseNumber(caseNumber);

            return await _dbContext.Cases
                .AnyAsync(c => c.CaseNumber.Value == valueObject, cancellationToken);

        }

        public async Task AddAsync(Case caseEntity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Cases.AddAsync(caseEntity, cancellationToken);
        }


    }
}
