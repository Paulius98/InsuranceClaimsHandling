using Claims.Domain.Entities.Audtiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.EntityConfigurations;

internal class CoverAuditEntityConfiguration : IEntityTypeConfiguration<CoverAudit>
{
    public void Configure(EntityTypeBuilder<CoverAudit> builder)
    {
        builder.ToTable("CoverAudits", "dbo");
        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.CoverId)
            .IsRequired();

        builder.Property(ca => ca.Created)
            .IsRequired();

        builder.Property(ca => ca.HttpRequestType)
            .HasConversion<string>()
            .HasMaxLength(EntityConfiguration.HttpRequestMethodTypeLength)
            .IsRequired();
    }
}