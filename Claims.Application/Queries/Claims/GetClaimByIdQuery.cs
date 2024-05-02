using Claims.Domain.Entities;
using MediatR;

namespace Claims.Application.Queries.Claims;

public record GetClaimByIdQuery(Guid Id) : IRequest<Claim>;
