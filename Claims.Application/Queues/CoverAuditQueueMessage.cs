using Claims.Application.Queues.Models;

namespace Claims.Application.Queues;

public class CoverAuditQueueMessage : QueueMessage
{
    public CoverAuditQueueMessage(CoverAuditPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
    {
        Payload = payload;
    }

    public CoverAuditPayload Payload { get; }
}