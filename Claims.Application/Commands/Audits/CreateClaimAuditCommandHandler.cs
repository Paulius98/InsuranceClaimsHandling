using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class CreateClaimAuditCommandHandler : INotificationHandler<CreateClaimAuditCommand>
{
    private readonly IClaimAuditRepository _repository;

    public CreateClaimAuditCommandHandler(IClaimAuditRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateClaimAuditCommand notification, CancellationToken cancellationToken)
    {
        var claimAudit = new ClaimAudit()
        {
            Created = DateTimeOffset.Now,
            HttpRequestType = notification.HttpRequestMethodType,
            ClaimId = notification.ClaimId
        };

        await _repository.AddAsync(claimAudit, cancellationToken);
    }
}
