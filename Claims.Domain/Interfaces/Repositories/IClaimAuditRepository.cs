using Claims.Domain.Entities.Audtiting;

namespace Claims.Domain.Interfaces.Repositories;

public interface IClaimAuditRepository
{
    Task AddAsync(ClaimAudit claimAudit, CancellationToken cancellationToken = default);
}
