using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Contexts;
using Claims.Domain.Interfaces.Repositories;

namespace Claims.Infrastructure.Persistence.Repositories;

public class ClaimAuditRepository : IClaimAuditRepository
{
    private readonly IAuditContext _context;

    public ClaimAuditRepository(IAuditContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ClaimAudit claimAudit, CancellationToken cancellationToken = default)
    {
        _context.ClaimAudits.Add(claimAudit);
        await _context.SaveContextChangesAsync(cancellationToken);
    }
}
