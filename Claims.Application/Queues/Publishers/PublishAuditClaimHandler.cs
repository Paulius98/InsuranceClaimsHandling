using Claims.Application.IntegrationEventHandlers.Models;
using Claims.Application.Queues;
using Claims.Domain.Enums;
using Claims.Domain.Events;
using Claims.Domain.Interfaces.Queues;
using MediatR;

namespace Claims.Application.IntegrationEventHandlers.Publishers;

public class PublishAuditClaimHandler :
    INotificationHandler<ClaimCreatedEvent>,
    INotificationHandler<ClaimDeletedEvent>
{
    private readonly IClaimAuditMessagePublisher _messagePublisher;

    public PublishAuditClaimHandler(IClaimAuditMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task Handle(ClaimCreatedEvent notification, CancellationToken cancellationToken)
    {
        await HandleAsync(notification, HttpRequestMethodType.POST, cancellationToken);
    }

    public async Task Handle(ClaimDeletedEvent notification, CancellationToken cancellationToken)
    {
        await HandleAsync(notification, HttpRequestMethodType.DELETE, cancellationToken);
    }

    private async Task HandleAsync(ClaimChangedEvent notification, HttpRequestMethodType type, CancellationToken cancellationToken)
    {
        var payload = new ClaimAuditPayload { ClaimId = notification.Claim.Id, RequestMethod = type };
        var message = new ClaimAuditQueueMessage(payload, notification.EmittedAt);

        await _messagePublisher.PublishAsync(message, cancellationToken);
    }
}
