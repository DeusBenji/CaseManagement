using Microsoft.EntityFrameworkCore;
using CaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Infrastructure.Peristence.Configuration
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {

        public void Configure(EntityTypeBuilder<Case> builder) 
        { 
            builder.ToTable("cases");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.ComplexProperty(c => c.CaseNumber, caseNumberbuilder => 
            {
                caseNumberbuilder.Property(cn => cn.Value)
                    .HasColumnName("case_number")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.ComplexProperty(c => c.Title, titleBuilder => 
            {
                titleBuilder.Property(t => t.Value)
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Priority)
                .HasColumnName("priority")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .HasColumnName("category_id");

            builder.Property(c => c.AssignedUserId)
                .HasColumnName("assigned_user_id");

            builder.Property(c => c.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Property(c => c.UpdatedAtUtc)
                .HasColumnName("updated_at_utc")
                .IsRequired();
            builder.Property(c => c.ClosedAtUtc)
                .HasColumnName("closed_at_utc");
            builder.Property(c => c.RowVersion)
                .HasColumnName("xmin")
                .IsRowVersion();
            builder.Ignore(c => c.DomainEvents);

            builder.HasMany(c => c.Comments)
                .WithOne()
                .HasForeignKey(cc => cc.CaseId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(c => c.Deadlines)
                .WithOne()
                .HasForeignKey(cd => cd.CaseId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex("case_number")
                .IsUnique();






        }

    }
}
