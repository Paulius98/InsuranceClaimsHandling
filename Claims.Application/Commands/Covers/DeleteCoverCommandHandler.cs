using Claims.Application.Exceptions;
using Claims.Application.Queries.Covers;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Covers;

public class DeleteCoverCommandHandler : INotificationHandler<DeleteCoverCommand>
{
    private readonly ICoverRepository _repository;
    private readonly IMediator _mediatior;

    public DeleteCoverCommandHandler(ICoverRepository repository, IMediator mediatior)
    {
        _repository = repository;
        _mediatior = mediatior;
    }

    public async Task Handle(DeleteCoverCommand notification, CancellationToken cancellationToken)
    {
        var cover = await _mediatior.Send(new GetCoverByIdQuery(notification.Id), cancellationToken);
        
        if (cover is null)
        {
            throw new CoverNotFoundException();
        }
        
        cover.Delete();
        await _repository.DeleteAsync(cover, cancellationToken);
    }
}
