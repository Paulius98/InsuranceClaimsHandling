using Claims.Domain.Entities;
using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Claims;

public record CreateClaimCommand(
    Guid CoverId,
    DateTimeOffset Created,
    string Name,
    ClaimType Type,
    decimal DamageCost) : IRequest<Claim>;
