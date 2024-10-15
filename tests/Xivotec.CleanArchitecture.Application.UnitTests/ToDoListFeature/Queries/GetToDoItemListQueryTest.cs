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
public class GetToDoItemListQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly TodoListFeatureTestObjects _testObjects;

    private readonly GetToDoItemListAllQueryHandler _sut;

    public GetToDoItemListQueryTest()
    {
        _sut = new GetToDoItemListAllQueryHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldFindCorrectListById_WhenListExists()
    {
        //Arrange
        var repo = Substitute.For<IRelationalRepository<ToDoItem>>();
        repo.GetAllAsync().Returns(_testObjects.ToDoItems);
        _unitOfWork.GetRelationalRepository<ToDoItem>().Returns(repo);

        _mapper.Map<ToDoItemDto>(Arg.Is(_testObjects.ToDoItems[0])).Returns(_testObjects.ToDoItemsDto[0]);
        _mapper.Map<ToDoItemDto>(Arg.Is(_testObjects.ToDoItems[1])).Returns(_testObjects.ToDoItemsDto[1]);

        //Act
        var result = await _sut.Handle(new GetToDoItemAllQuery(), CancellationToken.None);

        //Assert
        result.Count.Should().Be(_testObjects.ToDoItemsDto.Count);
        result.Should().BeEquivalentTo(_testObjects.ToDoItemsDto);
    }
}
