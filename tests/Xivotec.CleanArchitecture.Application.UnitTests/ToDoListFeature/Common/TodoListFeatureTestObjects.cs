using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;

internal class TodoListFeatureTestObjects
{
    public readonly List<ToDoItem> ToDoItems = new()
    {
        new ToDoItem()
        {
            Id = Guid.NewGuid(),
            Title = "Title 1",
            Note = "Note 1",
            Reminder = DateTimeOffset.MinValue,
            Done = false
        },
        new ToDoItem()
        {
            Id = Guid.NewGuid(),
            Title = "Title 2",
            Note = "Note 2",
            Reminder = DateTimeOffset.MaxValue,
            Done = true
        }
    };

    public readonly List<ToDoItemDto> ToDoItemsDto = new()
    {
        new ToDoItemDto()
        {
            Id = Guid.NewGuid(),
            Title = "Title 1",
            Note = "Note 1",
            Reminder = DateTimeOffset.MinValue,
            Done = false
        },
        new ToDoItemDto()
        {
            Id = Guid.NewGuid(),
            Title = "Title 2",
            Note = "Note 2",
            Reminder = DateTimeOffset.MaxValue,
            Done = true
        }
    };

    public readonly ToDoList ToDoList;
    public readonly ToDoListDto ToDoListDto;
    public readonly List<ToDoList> ToDoLists;
    public readonly List<ToDoListDto> ToDoListDtos;

    public TodoListFeatureTestObjects()
    {
        ToDoList = new ToDoList { Id = Guid.NewGuid(), Title = "List 1", ToDoItems = ToDoItems };
        ToDoLists = new() { ToDoList };
        ToDoListDto = new ToDoListDto
        {
            Id = ToDoList.Id,
            Title = ToDoList.Title,
            ToDoItems = ToDoItemsDto,
        };
        ToDoListDtos = new() { ToDoListDto };
    }
}