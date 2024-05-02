using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public class CoverCreatedEvent : CoverChangedEvent
{
    public CoverCreatedEvent(Cover cover, DateTimeOffset emittedAt) : base(cover, emittedAt)
    {
    }
}