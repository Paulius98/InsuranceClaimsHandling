using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class CreateCoverAuditCommandHandler : IRequestHandler<CreateCoverAuditCommand, CoverAudit>
{
    private readonly ICoverAuditRepository _repository;

    public CreateCoverAuditCommandHandler(ICoverAuditRepository repository)
    {
        _repository = repository;
    }

    public async Task<CoverAudit> Handle(CreateCoverAuditCommand notification, CancellationToken cancellationToken)
    {
        var coverAudit = new CoverAudit()
        {
            Created = DateTimeOffset.UtcNow,
            HttpRequestType = notification.HttpRequestMethodType,
            CoverId = notification.CoverId
        };

        await _repository.AddAsync(coverAudit, cancellationToken);

        return coverAudit;
    }
}
