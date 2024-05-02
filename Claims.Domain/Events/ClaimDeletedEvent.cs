using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public class ClaimDeletedEvent : ClaimChangedEvent
{
    public ClaimDeletedEvent(Claim claim, DateTimeOffset emittedAt) : base(claim, emittedAt)
    {
    }
}
