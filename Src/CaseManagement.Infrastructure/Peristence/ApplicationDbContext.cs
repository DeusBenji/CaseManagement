using CaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Infrastructure.Peristence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Case> Cases => Set<Case>();
        public DbSet<CaseDeadline> CaseDeadlines => Set<CaseDeadline>();
        public DbSet<CaseComment> CaseComments => Set<CaseComment>();

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }   

    }
}
