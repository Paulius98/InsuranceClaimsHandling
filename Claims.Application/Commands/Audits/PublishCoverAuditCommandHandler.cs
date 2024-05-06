using Claims.Application.Queues;
using Claims.Application.Queues.Models;
using Claims.Domain.Interfaces.Queues;
using MediatR;

namespace Claims.Application.Commands.Audits;

public class PublishCoverAuditCommandHandler : INotificationHandler<PublishCoverAuditCommand>
{
    private readonly ICoverAuditMessagePublisher _messagePublisher;

    public PublishCoverAuditCommandHandler(ICoverAuditMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task Handle(PublishCoverAuditCommand notification, CancellationToken cancellationToken)
    {
        var payload = new CoverAuditPayload { CoverId = notification.CoverId, RequestMethod = notification.HttpRequestMethodType };
        var message = new CoverAuditQueueMessage(payload, DateTimeOffset.UtcNow);

        await _messagePublisher.PublishAsync(message, cancellationToken);
    }
}
