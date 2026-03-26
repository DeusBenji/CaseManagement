using CaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Infrastructure.Peristence.Configuration
{
    public class CaseDeadlineConfiguration : IEntityTypeConfiguration<CaseDeadline>
    {
        public void Configure(EntityTypeBuilder<CaseDeadline> builder)
        {
            builder.ToTable("case_deadlines");

            builder.HasKey(cd => cd.Id);

            builder.Property(cd => cd.Id)
                .ValueGeneratedNever();

            builder.Property(cd => cd.CaseId)
                .HasColumnName("case_id")
                .IsRequired();

            builder.Property(cd => cd.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(cd => cd.DueDateUtc)
                .HasColumnName("due_date_utc")
                .IsRequired();
            builder.Property(cd => cd.IsCompleted)
                .HasColumnName("is_completed")
                .IsRequired();
            builder.Property(cd => cd.CompletedAtUtc)
                .HasColumnName("completed_at_utc");

            builder.Ignore(cd => cd.DomainEvents);



        }
    }
}