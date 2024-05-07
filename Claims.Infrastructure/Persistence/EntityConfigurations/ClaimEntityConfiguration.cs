using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Claims.Infrastructure.Persistence.EntityConfigurations;

internal class ClaimEntityConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToCollection(EntityConfiguration.ClaimCollectionName);
    }
}