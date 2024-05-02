using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Contexts;
using Claims.Infrastructure.Persistence.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence;

public class ClaimsContext : DbContext, IClaimsContext
{
    private readonly IMediator _mediator;

    public DbSet<Claim> Claims { get; init; }
    public DbSet<Cover> Covers { get; init; }

    public ClaimsContext(IMediator mediator, DbContextOptions options) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClaimEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CoverEntityConfiguration());
    }

    public async Task SaveContextChangesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
    
    public async Task EmitEventsAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : AggregateRoot
    {
        foreach (var domainEvent in entity.DomainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
