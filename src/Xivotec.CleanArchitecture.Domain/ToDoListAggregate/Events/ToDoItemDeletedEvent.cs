﻿using Xivotec.CleanArchitecture.Domain.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;

public sealed class ToDoItemDeletedEvent(ToDoItem value)
    : DomainEvent<ToDoItem>(value)
{
}
