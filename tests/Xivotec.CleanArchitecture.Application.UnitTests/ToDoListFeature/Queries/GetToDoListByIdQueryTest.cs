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

public class GetToDoListByIdQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly GetToDoListByIdHandler _sut;

    public GetToDoListByIdQueryTest()
    {
        _sut = new GetToDoListByIdHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldFindCorrectListById_WhenListExists()
    {
        //Arrange
        var repo = Substitute.For<IRepository<ToDoList>>();
        repo.GetByIdAsync(Arg.Is(_testObjects.ToDoList.Id)).Returns(_testObjects.ToDoList);
        _unitOfWork.GetRepository<ToDoList>().Returns(repo);

        _mapper.Map<ToDoListDto>(Arg.Is(_testObjects.ToDoList)).Returns(_testObjects.ToDoListDto);

        //Act
        var result = await _sut.Handle(new GetToDoListByIdQuery(_testObjects.ToDoList.Id), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(_testObjects.ToDoList.Id);

        await repo.Received(1).GetByIdAsync(_testObjects.ToDoList.Id);
    }
}