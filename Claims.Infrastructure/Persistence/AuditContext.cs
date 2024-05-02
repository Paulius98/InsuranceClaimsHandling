using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Contexts;
using Claims.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence;

public class AuditContext : DbContext, IAuditContext
{
    public DbSet<ClaimAudit> ClaimAudits { get; init; }
    public DbSet<CoverAudit> CoverAudits { get; init; }

    public AuditContext(DbContextOptions<AuditContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClaimAuditEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CoverAuditEntityConfiguration());
    }

    public async Task SaveContextChangesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync();
    }
}
