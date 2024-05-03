using MediatR;

namespace Claims.Domain.Events;

public abstract class Event : INotification
{
    protected Event(DateTimeOffset emittedAt)
    {
        EmittedAt = emittedAt;
    }

    public DateTimeOffset EmittedAt { get; }
}
