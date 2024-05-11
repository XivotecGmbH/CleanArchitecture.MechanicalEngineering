using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Common;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ToDoListFeature.Queries;

public class GetToDoListListQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly GetToDoListListHandler _sut;

    public GetToDoListListQueryTest()
    {
        _sut = new GetToDoListListHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoToDoListsExist()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoList>>();
        repo.GetAllAsync().Returns(new List<ToDoList>());

        _unitOfWork.GetRepository<ToDoList>().Returns(repo);

        //Act
        var result = await _sut.Handle(new GetToDoListAllQuery(), CancellationToken.None);

        //Assert
        result.Should().BeEmpty();

        await repo.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectListOfLists_WhenMultipleToDoListsExist()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoList>>();
        repo.GetAllAsync().Returns(_testObjects.ToDoLists);
        _unitOfWork.GetRepository<ToDoList>().Returns(repo);

        _mapper.Map<ToDoListDto>(Arg.Is(_testObjects.ToDoLists[0])).Returns(_testObjects.ToDoListDto);

        //Act
        var result = await _sut.Handle(new GetToDoListAllQuery(), CancellationToken.None);

        //Assert
        result.Count.Should().Be(_testObjects.ToDoListDtos.Count);
        result.Should().BeEquivalentTo(_testObjects.ToDoListDtos);

        await repo.Received(1).GetAllAsync();
    }

}