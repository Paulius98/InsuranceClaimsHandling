using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Contexts;
using Claims.Domain.Interfaces.Repositories;

namespace Claims.Infrastructure.Persistence.Repositories;

public class CoverAuditRepository : ICoverAuditRepository
{
    private readonly IAuditContext _context;

    public CoverAuditRepository(IAuditContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CoverAudit coverAudit, CancellationToken cancellationToken = default)
    {
        _context.CoverAudits.Add(coverAudit);
        await _context.SaveContextChangesAsync();
    }
}
