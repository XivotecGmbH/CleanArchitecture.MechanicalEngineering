using AutoMapper;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Services.DomainDispatcher;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Events;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class DeleteToDoListCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IDomainEventDispatcher _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly DeleteToDoListHandler _sut;

    public DeleteToDoListCommandTest()
    {
        _sut = new DeleteToDoListHandler(
            _mapper,
            _unitOfWork,
            _domainEventDispatcher);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoList>>();

        _unitOfWork.GetRepository<ToDoList>().Returns(repo);
        _mapper.Map<ToDoList>(Arg.Is(_testObjects.ToDoListDto)).Returns(_testObjects.ToDoList);

        //Act
        await _sut.Handle(new DeleteToDoListCommand(_testObjects.ToDoListDto), CancellationToken.None);

        //Assert
        await _domainEventDispatcher.Received(1).RaiseDomainEvent(Arg.Any<ToDoListDeletedEvent>());
        await repo.Received(1).DeleteAsync(Arg.Is(_testObjects.ToDoList));
    }
}