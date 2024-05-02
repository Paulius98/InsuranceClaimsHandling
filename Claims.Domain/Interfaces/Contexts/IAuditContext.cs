using Claims.Domain.Entities;
using Claims.Domain.Entities.Audtiting;
using Microsoft.EntityFrameworkCore;

namespace Claims.Domain.Interfaces.Contexts;

public interface IAuditContext : IContext
{
    public DbSet<ClaimAudit> ClaimAudits { get; init; }
    public DbSet<CoverAudit> CoverAudits { get; init; }
}
