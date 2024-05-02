using Claims.Application.IntegrationEventHandlers.Models;

namespace Claims.Application.Queues;

public class ClaimAuditQueueMessage : QueueMessage
{
    public ClaimAuditQueueMessage(ClaimAuditPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
    {
        Payload = payload;
    }

    public ClaimAuditPayload Payload { get; }
}
