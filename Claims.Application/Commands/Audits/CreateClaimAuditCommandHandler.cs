using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class CreateClaimAuditCommandHandler : IRequestHandler<CreateClaimAuditCommand, ClaimAudit>
{
    private readonly IClaimAuditRepository _repository;

    public CreateClaimAuditCommandHandler(IClaimAuditRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClaimAudit> Handle(CreateClaimAuditCommand notification, CancellationToken cancellationToken)
    {
        var claimAudit = new ClaimAudit()
        {
            Created = DateTimeOffset.UtcNow,
            HttpRequestType = notification.HttpRequestMethodType,
            ClaimId = notification.ClaimId
        };

        await _repository.AddAsync(claimAudit, cancellationToken);

        return claimAudit;
    }
}
