using Claims.Domain.Entities;

namespace Claims.Domain.Interfaces.Repositories
{
    public interface IClaimsRepository
    {
        Task<IEnumerable<Claim>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Claim claim, CancellationToken cancellationToken = default);
        Task DeleteAsync(Claim claim, CancellationToken cancellationToken = default);
    }
}
