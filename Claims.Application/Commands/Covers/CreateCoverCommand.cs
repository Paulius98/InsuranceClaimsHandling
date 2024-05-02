using Claims.Domain.Entities;
using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Covers;

public record CreateCoverCommand(
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    CoverType Type
    ) : IRequest<Cover>;
