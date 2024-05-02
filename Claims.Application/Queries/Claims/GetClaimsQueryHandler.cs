using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Queries.Claims;

public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IEnumerable<Claim>>
{
    private readonly IClaimsRepository _claimRepository;

    public GetClaimsQueryHandler(IClaimsRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task<IEnumerable<Claim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        return await _claimRepository.GetAllAsync();
    }
}
