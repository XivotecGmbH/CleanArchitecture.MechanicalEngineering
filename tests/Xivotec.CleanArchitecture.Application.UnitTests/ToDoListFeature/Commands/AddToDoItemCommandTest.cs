using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class AddToDoItemCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IDomainEventDispatcher _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly AddToDoItemHandler _sut;

    public AddToDoItemCommandTest()
    {
        _sut = new AddToDoItemHandler(_mapper,
            _unitOfWork,
            _domainEventDispatcher);

        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnNewToDoItem_WhenTodoItemPresent()
    {
        //Arrange
        var repo = Substitute.For<IRelationalRepository<ToDoItem>>();

        _mapper.Map<ToDoItem>(Arg.Is(_testObjects.ToDoItemsDto[0])).Returns(_testObjects.ToDoItems[0]);
        _mapper.Map<ToDoItemDto>(Arg.Is(_testObjects.ToDoItems[0])).Returns(_testObjects.ToDoItemsDto[0]);

        _unitOfWork.GetRelationalRepository<ToDoItem>().Returns(repo);

        //Act
        var result = await _sut.Handle(new AddToDoItemCommand(_testObjects.ToDoItemsDto[0]), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.ToDoItemsDto[0]);

        await repo.Received(1).AddAsync(Arg.Is(_testObjects.ToDoItems[0]));
    }
}