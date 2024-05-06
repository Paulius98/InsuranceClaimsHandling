using Claims.Application.Exceptions;
using Claims.Application.Queries.Claims;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Claims;

public class DeleteClaimCommandHandler : INotificationHandler<DeleteClaimCommand>
{
    private readonly IClaimsRepository _repository;
    private readonly IMediator _mediator;

    public DeleteClaimCommandHandler(IClaimsRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task Handle(DeleteClaimCommand notification, CancellationToken cancellationToken)
    {
        var claim = await _mediator.Send(new GetClaimByIdQuery(notification.Id), cancellationToken);
        if (claim is null)
        {
            throw new CoverNotFoundException();
        }

        await _repository.DeleteAsync(claim, cancellationToken);
    }
}
