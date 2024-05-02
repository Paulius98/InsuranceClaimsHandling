using Claims.Domain.Entities;
using MediatR;

namespace Claims.Application.Queries.Covers
{
    public record GetCoversQuery() : IRequest<IEnumerable<Cover>>;
}
