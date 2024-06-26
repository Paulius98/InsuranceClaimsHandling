﻿namespace Claims.Application.Queues.Models;

public abstract class QueueMessage
{
    protected QueueMessage(DateTimeOffset emittedAt)
    {
        MessageId = Guid.NewGuid();
        EmittedAt = emittedAt;
    }

    protected QueueMessage()
    {
    }

    public Guid MessageId { get; init; }
    public DateTimeOffset EmittedAt { get; init; }
}
