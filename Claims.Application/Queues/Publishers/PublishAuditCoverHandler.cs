using Claims.Application.Queues.Models;
using Claims.Domain.Enums;
using Claims.Domain.Events;
using Claims.Domain.Interfaces.Queues;
using MediatR;

namespace Claims.Application.Queues.Publishers;

public class PublishAuditCoverHandler :
    INotificationHandler<CoverCreatedEvent>,
    INotificationHandler<CoverDeletedEvent>
{
    private readonly ICoverAuditMessagePublisher _messagePublisher;

public PublishAuditCoverHandler(ICoverAuditMessagePublisher messagePublisher)
{
    _messagePublisher = messagePublisher;
}

public async Task Handle(CoverCreatedEvent notification, CancellationToken cancellationToken)
{
    await HandleAsync(notification, HttpRequestMethodType.POST, cancellationToken);
}

public async Task Handle(CoverDeletedEvent notification, CancellationToken cancellationToken)
{
    await HandleAsync(notification, HttpRequestMethodType.DELETE, cancellationToken);
}

private async Task HandleAsync(CoverChangedEvent notification, HttpRequestMethodType type, CancellationToken cancellationToken)
{
    var payload = new CoverAuditPayload { CoverId = notification.Cover.Id, RequestMethod = type };
    var message = new CoverAuditQueueMessage(payload, notification.EmittedAt);

    await _messagePublisher.PublishAsync(message, cancellationToken);
}
}
