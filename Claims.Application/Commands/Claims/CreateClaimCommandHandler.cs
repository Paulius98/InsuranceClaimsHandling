using Claims.Application.Queries.Covers;
using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Claims;

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, Claim>
{
    private readonly IClaimsRepository _repository;
    private readonly IMediator _mediator;

    public CreateClaimCommandHandler(IClaimsRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var claim = Claim.Create(
            request.CoverId,
            request.Created,
            request.Name,
            request.Type,
            request.DamageCost);

        var cover = await _mediator.Send(new GetCoverByIdQuery(request.CoverId), cancellationToken);

        claim.ValidateDamageCost();
        claim.ValidateDate(cover.StartDate, cover.EndDate);

        await _repository.AddAsync(claim, cancellationToken);

        return claim;
    }
}
