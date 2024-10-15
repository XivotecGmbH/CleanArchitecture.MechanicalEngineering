using AutoMapper;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class UpdateToDoListCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly UpdateToDoListHandler _sut;

    public UpdateToDoListCommandTest()
    {
        _sut = new UpdateToDoListHandler(_mapper,
            _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<IRelationalRepository<ToDoList>>();

        _mapper.Map<ToDoList>(Arg.Is(_testObjects.ToDoListDto)).Returns(_testObjects.ToDoList);
        _unitOfWork.GetRelationalRepository<ToDoList>().Returns(repo);

        //Act
        await _sut.Handle(new UpdateToDoListCommand(_testObjects.ToDoListDto), CancellationToken.None);

        //Assert
        await repo.Received(1).UpdateAsync(Arg.Is(_testObjects.ToDoList));
    }
}