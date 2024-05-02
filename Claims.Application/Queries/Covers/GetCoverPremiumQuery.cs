using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Queries.Covers;

public record GetCoverPremiumQuery(
    DateTimeOffset StartDate, 
    DateTimeOffset EndDate,
    CoverType CoverType) : IRequest<decimal>;
