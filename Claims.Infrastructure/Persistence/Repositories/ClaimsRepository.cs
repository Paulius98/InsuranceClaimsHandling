using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Contexts;
using Claims.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence.Repositories;

public class ClaimsRepository : IClaimsRepository
{
    private readonly IClaimsContext _context;

    public ClaimsRepository(IClaimsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Claim>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Claims.ToListAsync(cancellationToken);
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Claims
            .Where(claim => claim.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        _context.Claims.Add(claim);
        await _context.SaveContextChangesAsync(cancellationToken);
        await _context.EmitEventsAsync(claim, cancellationToken);
    }

    public async Task DeleteAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        _context.Claims.Remove(claim);
        await _context.SaveContextChangesAsync();
        await _context.EmitEventsAsync(claim);
    }
}
