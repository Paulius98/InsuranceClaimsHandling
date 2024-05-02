using Claims.Domain.Entities;
using MediatR;

namespace Claims.Application.Queries.Covers;

public record GetCoverByIdQuery(Guid Id) : IRequest<Cover>;
