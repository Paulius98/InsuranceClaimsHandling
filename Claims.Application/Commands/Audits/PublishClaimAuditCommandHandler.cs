using Claims.Application.IntegrationEventHandlers.Models;
using Claims.Application.Queues.Models;
using Claims.Domain.Interfaces.Queues;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class PublishClaimAuditCommandHandler : INotificationHandler<PublishClaimAuditCommand>
{
    private readonly IClaimAuditMessagePublisher _messagePublisher;

    public PublishClaimAuditCommandHandler(IClaimAuditMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task Handle(PublishClaimAuditCommand notification, CancellationToken cancellationToken)
    {
        var payload = new ClaimAuditPayload { ClaimId = notification.ClaimId, RequestMethod = notification.HttpRequestMethodType };
        var message = new ClaimAuditQueueMessage(payload, DateTimeOffset.UtcNow);

        await _messagePublisher.PublishAsync(message, cancellationToken);
    }
}
