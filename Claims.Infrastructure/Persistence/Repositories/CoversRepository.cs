using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Contexts;
using Claims.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence.Repositories;

public class CoversRepository : ICoverRepository
{
    private readonly IClaimsContext _context;

    public CoversRepository(IClaimsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cover>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Covers.ToListAsync();
    }

    public async Task<Cover?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Covers
            .Where(cover => cover.Id == id)
            .SingleOrDefaultAsync();
    }

    public async Task AddAsync(Cover cover, CancellationToken cancellationToken = default)
    {
        _context.Covers.Add(cover);
        await _context.SaveContextChangesAsync();
    }

    public async Task DeleteAsync(Cover cover, CancellationToken cancellationToken = default)
    {
        _context.Covers.Remove(cover);
        await _context.SaveContextChangesAsync();
    }
}
