using Claims.Domain.Entities;
using MediatR;

namespace Claims.Application.Queries.Claims
{
    public record GetClaimsQuery() : IRequest<IEnumerable<Claim>>;
}
