using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Contexts;
using Claims.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence;

public class ClaimsContext : DbContext, IClaimsContext
{
    public DbSet<Claim> Claims { get; init; }
    public DbSet<Cover> Covers { get; init; }

    public ClaimsContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClaimEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CoverEntityConfiguration());
    }

    public async Task SaveContextChangesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}
