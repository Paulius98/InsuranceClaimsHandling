using Claims.Application.Exceptions;
using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Queries.Claims;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, Claim>
{
    private readonly IClaimsRepository _claimsRepository;
    
    public GetClaimByIdQueryHandler(IClaimsRepository claimsRepository)
    {
        _claimsRepository = claimsRepository;
    }

    public async Task<Claim> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        var claim = await _claimsRepository.GetByIdAsync(request.Id);
        
        if (claim is null)
        {
            throw new ClaimNotFoundException();
        }

        return claim;
    }
}
