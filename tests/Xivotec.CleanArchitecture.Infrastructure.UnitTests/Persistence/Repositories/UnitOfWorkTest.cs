using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort;
using Xivotec.CleanArchitecture.Infrastructure.Persistence;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common;
using Xivotec.CleanArchitecture.Infrastructure.Persistence.Common.Interfaces;
using Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Persistence.Repositories;

public class UnitOfWorkTest
{
    private readonly IDataContext _context = Substitute.For<IDataContext>();
    private readonly List<ToDoItem> _testEntities;

    private IUnitOfWork _sut;

    public UnitOfWorkTest()
    {
        IEnumerable<IPersistentRepository> persistentRepositories = new List<IPersistentRepository>();
        IEnumerable<IRuntimeRepository> runtimeRepositories = new List<IRuntimeRepository> { new ItemRunRepo() };

        _sut = new UnitOfWork(_context, persistentRepositories, runtimeRepositories);


        _testEntities =
        [
            new ToDoItem { Title = "Sophie" },
            new ToDoItem { Title = "Tom" }
        ];
    }

    [Fact]
    public void GetRelationalRepository_ShouldThrowException_WhenInstanceCanNotBeReturned()
    {
        //Arrange
        _sut = new UnitOfWork(_context, new List<IPersistentRepository>(), new List<IRuntimeRepository>());

        //Act 
        var action = () => _sut.GetRelationalRepository<ToDoItem>();

        //Assert
        action.Should().Throw<RepositoryNotFoundException>();
    }

    [Fact]
    public void GetRelationalRepository_ShouldReturnInstance_WhenRepositoryExists()
    {
        //Act
        var result = _sut.GetRelationalRepository<ToDoItem>();

        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void GetRelationalRepository_ShouldReturnInstance_WhenRelationalAndTimeSeriesRepositoryExist()
    {
        //Arrange
        var configuration = Substitute.For<IConfiguration>();
        var logger = Substitute.For<ILogger<InfluxDbPortDataContext>>();
        var mapper = Substitute.For<IMapper>();
        var context = Substitute.For<InfluxDbPortDataContext>(configuration, logger);

        _sut = new UnitOfWork(_context, new List<IPersistentRepository>
        {
            new TestTimeSeriesRepository(mapper, context, configuration)
        }, new List<IRuntimeRepository>
        {
            new ItemRunRepo()
        });

        //Act
        var result = _sut.GetRelationalRepository<ToDoItem>();

        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetRelationalRepository_ShouldReturnTwoItems_WhenRepositoryItemWereAdded()
    {
        // Arrange
        var repo = _sut.GetRelationalRepository<ToDoItem>();
        await repo.AddAsync(_testEntities[0]);
        await repo.AddAsync(_testEntities[1]);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        result.Count.Should().Be(2);
    }

    [Fact]
    public void GetTimeSeriesRepository_ShouldThrowException_WhenInstanceCanNotBeReturned()
    {
        //Arrange
        _sut = new UnitOfWork(_context, new List<IPersistentRepository>(), new List<IRuntimeRepository>());

        //Act 
        var action = () => _sut.GetTimeSeriesRepository<TestTimeSeriesEntry>();

        //Assert
        action.Should().Throw<RepositoryNotFoundException>();
    }

    [Fact]
    public void GetTimeSeriesRepository_ShouldReturnInstance_WhenRepositoryExists()
    {
        //Arrange
        var configuration = Substitute.For<IConfiguration>();
        var logger = Substitute.For<ILogger<InfluxDbPortDataContext>>();
        var mapper = Substitute.For<IMapper>();
        var context = Substitute.For<InfluxDbPortDataContext>(configuration, logger);

        _sut = new UnitOfWork(_context, new List<IPersistentRepository>()
        {
            new TestTimeSeriesRepository(mapper, context, configuration)
        }, new List<IRuntimeRepository>());

        //Act
        var result = _sut.GetTimeSeriesRepository<TemperatureEntry>();

        //Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void GetTimeSeriesRepository_ShouldReturnInstance_WhenRelationalAndTimeSeriesRepositoryExist()
    {
        //Arrange
        var configuration = Substitute.For<IConfiguration>();
        var logger = Substitute.For<ILogger<InfluxDbPortDataContext>>();
        var mapper = Substitute.For<IMapper>();
        var context = Substitute.For<InfluxDbPortDataContext>(configuration, logger);

        _sut = new UnitOfWork(_context, new List<IPersistentRepository>
        {
            new TestTimeSeriesRepository(mapper, context, configuration)
        }, new List<IRuntimeRepository>
        {
            new ItemRunRepo()
        });

        //Act
        var result = _sut.GetTimeSeriesRepository<TemperatureEntry>();

        //Assert
        result.Should().NotBeNull();
    }
}

internal class ItemRunRepo : RuntimeRepository<ToDoItem>;