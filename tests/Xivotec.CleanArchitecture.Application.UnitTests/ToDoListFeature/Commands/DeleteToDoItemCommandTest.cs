using AutoMapper;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class DeleteToDoItemCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly DeleteToDoItemHandler _sut;

    public DeleteToDoItemCommandTest()
    {
        _sut = new DeleteToDoItemHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoItem>>();

        _unitOfWork.GetRepository<ToDoItem>().Returns(repo);
        _mapper.Map<ToDoItem>(Arg.Is(_testObjects.ToDoItemsDto[0])).Returns(_testObjects.ToDoItems[0]);

        //Act
        await _sut.Handle(new DeleteToDoItemCommand(_testObjects.ToDoItemsDto[0]), CancellationToken.None);

        //Assert
        await repo.Received(1).DeleteAsync(Arg.Is(_testObjects.ToDoItems[0]));
    }
}