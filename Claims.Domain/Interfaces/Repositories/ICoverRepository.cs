using Claims.Domain.Entities;

namespace Claims.Domain.Interfaces.Repositories
{
    public interface ICoverRepository
    {
        Task<IEnumerable<Cover>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Cover?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Cover claim, CancellationToken cancellationToken = default);
        Task DeleteAsync(Cover id, CancellationToken cancellationToken = default);
    }
}
