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
                //.Include(c => c.Comments)
                //.Include(c => c.Deadlines)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Case?> GetByCaseNumberAsync(string caseNumber, CancellationToken cancellationToken = default)
        {
            var valueObject = new CaseNumber(caseNumber);

            return await _dbContext.Cases
                //.Include(c => c.Comments)
                //.Include(c => c.Deadlines)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CaseNumber == valueObject, cancellationToken);
        }

        public async Task<bool> ExistsByCaseNumberAsync(string caseNumber, CancellationToken cancellationToken = default)
        {
            var valueObject = new CaseNumber(caseNumber);

            return await _dbContext.Cases
                .AnyAsync(c => c.CaseNumber == valueObject, cancellationToken);

        }

        public async Task AddAsync(Case caseEntity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Cases.AddAsync(caseEntity, cancellationToken);
        }
        public async Task<IReadOnlyList<Case>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cases
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAtUtc)
                .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Case>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cases
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAtUtc)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Cases.CountAsync(cancellationToken);
        }


    }
}
