namespace Claims.Application.Queues.Models;

public class CoverAuditQueueMessage : QueueMessage
{
    public CoverAuditQueueMessage(CoverAuditPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
    {
        Payload = payload;
    }

    public CoverAuditPayload Payload { get; }
}