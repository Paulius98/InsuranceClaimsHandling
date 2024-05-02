using Claims.Domain.Entities.Audtiting;

namespace Claims.Domain.Interfaces.Repositories;

public interface ICoverAuditRepository
{
    Task AddAsync(CoverAudit coverAudit, CancellationToken cancellationToken = default);
}
