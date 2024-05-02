using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public class CoverDeletedEvent : CoverChangedEvent
{
    public CoverDeletedEvent(Cover cover, DateTimeOffset emittedAt) : base(cover, emittedAt)
    {
    }
}