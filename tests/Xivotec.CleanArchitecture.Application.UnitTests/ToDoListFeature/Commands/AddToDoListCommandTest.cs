using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Commands;

public class AddToDoListCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly AddToDoListHandler _sut;

    public AddToDoListCommandTest()
    {
        _sut = new AddToDoListHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnNewToDoList_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoList>>();

        _mapper.Map<ToDoList>(Arg.Is(_testObjects.ToDoListDto)).Returns(_testObjects.ToDoList);
        _mapper.Map<ToDoListDto>(Arg.Is(_testObjects.ToDoList)).Returns(_testObjects.ToDoListDto);

        _unitOfWork.GetRepository<ToDoList>().Returns(repo);

        //Act
        var result = await _sut.Handle(new AddToDoListCommand(_testObjects.ToDoListDto), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.ToDoListDto);

        await repo.Received(1).AddAsync(Arg.Is(_testObjects.ToDoList));
    }
}