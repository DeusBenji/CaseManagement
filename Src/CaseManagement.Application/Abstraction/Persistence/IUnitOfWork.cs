namespace CaseManagement.Application.Abstraction.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
