using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class CreateCoverAuditCommandHandler : INotificationHandler<CreateCoverAuditCommand>
{
    private readonly ICoverAuditRepository _repository;

    public CreateCoverAuditCommandHandler(ICoverAuditRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(CreateCoverAuditCommand notification, CancellationToken cancellationToken)
    {
        var coverAudit = new CoverAudit()
        {
            Created = DateTimeOffset.Now,
            HttpRequestType = notification.HttpRequestMethodType,
            CoverId = notification.CoverId
        };

        await _repository.AddAsync(coverAudit, cancellationToken);
    }
}
