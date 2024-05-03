﻿using Claims.Domain.Entities;

namespace Claims.Domain.Events;

public abstract class CoverChangedEvent : Event
{
    public Cover Cover { get; }

    protected CoverChangedEvent(Cover cover, DateTimeOffset emittedAt) : base(emittedAt)
    {
        Cover = cover;
    }
}