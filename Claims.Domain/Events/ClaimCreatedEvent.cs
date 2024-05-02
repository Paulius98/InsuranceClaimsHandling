using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public class ClaimCreatedEvent : ClaimChangedEvent
{
    public ClaimCreatedEvent(Claim claim, DateTimeOffset emittedAt) : base(claim, emittedAt)
    {
    }
}
