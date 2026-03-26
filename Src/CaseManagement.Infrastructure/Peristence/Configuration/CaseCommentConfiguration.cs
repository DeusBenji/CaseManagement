using CaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseManagement.Infrastructure.Peristence.Configuration
{
    public class CaseCommentConfiguration : IEntityTypeConfiguration<CaseComment>
    {
        public void Configure(EntityTypeBuilder<CaseComment> builder)
        {
            builder.ToTable("case_comments");

            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Id)
                .ValueGeneratedNever();

            builder.Property(cc => cc.CaseId)
                .HasColumnName("case_id")
                .IsRequired();

            builder.Property(cc => cc.CaseId)
                .HasColumnName("case_id")
                .IsRequired();

            builder.Property(cc => cc.AuthorUserId)
                .HasColumnName("author_user_id")
                .IsRequired();

            builder.Property(cc => cc.Text)
                .HasColumnName("text")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(cc => cc.isInternal)
                .HasColumnName("is_internal")
                .IsRequired();

            builder.Property (cc => cc.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired();

            builder.Ignore(cc => cc.DomainEvents);


        }

    }
}
