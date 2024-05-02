using Claims.Domain.Calculations;
using MediatR;

namespace Claims.Application.Queries.Covers;

public class GetCoverPremiumQueryHandler : IRequestHandler<GetCoverPremiumQuery, decimal>
{
    public Task<decimal> Handle(GetCoverPremiumQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            PremiumCalculator.Compute(
                request.StartDate,
                request.EndDate, 
                request.CoverType)
        );
    }
}
