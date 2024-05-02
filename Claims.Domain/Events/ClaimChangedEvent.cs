using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public abstract class ClaimChangedEvent : Event
{
    public Claim Claim { get; }

    protected ClaimChangedEvent(Claim claim, DateTimeOffset emittedAt) : base (emittedAt)
    {
        Claim = claim;
    }
}
