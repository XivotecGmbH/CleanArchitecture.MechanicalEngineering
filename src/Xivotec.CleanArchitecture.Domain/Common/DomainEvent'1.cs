﻿using Xivotec.CleanArchitecture.Domain.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Domain.Common;

public abstract class DomainEvent<TEntityType> : IDomainEvent where TEntityType : Entity
{
    /// <summary>
    /// Entity the DomainEvent is associate with
    /// </summary>
    public TEntityType Value { get; private set; }

    /// <summary>
    /// Time stamp the DomainEvent happened
    /// </summary>
    public DateTimeOffset TimeOccured { get; private set; }

    protected DomainEvent(TEntityType value)
    {
        Value = value;
        TimeOccured = DateTimeOffset.Now;
    }
}
