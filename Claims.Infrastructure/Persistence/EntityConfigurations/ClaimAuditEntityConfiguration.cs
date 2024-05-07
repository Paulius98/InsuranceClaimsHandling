using Claims.Domain.Entities.Audtiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.EntityConfigurations;

internal class ClaimAuditEntityConfiguration : IEntityTypeConfiguration<ClaimAudit>
{
    public void Configure(EntityTypeBuilder<ClaimAudit> builder)
    {
        builder.ToTable(EntityConfiguration.ClaimAuditTableName, EntityConfiguration.DefaultSchema);
        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.ClaimId)
            .IsRequired();

        builder.Property(ca => ca.Created)
            .IsRequired();

        builder.Property(ca => ca.HttpRequestType)
            .HasConversion<string>()
            .HasMaxLength(EntityConfiguration.HttpRequestMethodTypeLength)
            .IsRequired();
    }
}
