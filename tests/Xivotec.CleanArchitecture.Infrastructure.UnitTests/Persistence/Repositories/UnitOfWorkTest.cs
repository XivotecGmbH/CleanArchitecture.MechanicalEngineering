using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.Persistence;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories;

public class UnitOfWorkTest
{
    private readonly IDataContext _context = Substitute.For<IDataContext>();
    private readonly List<ToDoItem> _testEntities;

    private IUnitOfWork _sut;

    public UnitOfWorkTest()
    {
        IEnumerable<IPersistentRepository> persRepo = new List<IPersistentRepository>();
        IEnumerable<IRuntimeRepository> runRepo = new List<IRuntimeRepository>() { new ItemRunRepo() };

        _sut = new UnitOfWork(_context, persRepo, runRepo);


        _testEntities = new()
        {
            new ToDoItem() { Title = "Sophie" },
            new ToDoItem() { Title = "Tom" }
        };
    }

    [Fact]
    public void GetRepository_ShouldThrowException_WhenInstanceCanNotBeReturned()
    {
        //Arrange
        _sut = new UnitOfWork(_context, new List<IPersistentRepository>(), new List<IRuntimeRepository>());

        //Act 
        var action = () => _sut.GetRepository<ToDoItem>();

        //Assert
        action.Should().Throw<RepositoryNotFoundException>();
    }

    [Fact]
    public void GetRepository_ShouldReturnInstance_WhenRepositoryExist()
    {
        //Act
        var result = _sut.GetRepository<ToDoItem>();
        var result2 = _sut.GetRepository<ToDoItem>();

        //Assert
        result.Should().NotBeNull();
        result2.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRepository_ShouldReturnTwoItems_WhenRepositoryItemWereAdded()
    {
        // Arrange
        var repo = _sut.GetRepository<ToDoItem>();
        await repo.AddAsync(_testEntities[0]);
        await repo.AddAsync(_testEntities[1]);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        result.Count.Should().Be(2);
    }
}

class ItemRunRepo : RuntimeRepository<ToDoItem>
{
}