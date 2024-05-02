﻿using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claims.Domain.Interfaces.Contexts;

public interface IClaimsContext : IContext
{
    DbSet<Claim> Claims { get; init; }
    DbSet<Cover> Covers { get; init; }

    Task EmitEventsAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : AggregateRoot;
}
