using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Common;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Commands;

public class AddTemperatureEntriesBatchedCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TemperatureFeatureTestObjects _testObjects = new();

    private readonly AddTemperatureEntriesBatchedHandler _sut;

    public AddTemperatureEntriesBatchedCommandTest()
    {
        _sut = new AddTemperatureEntriesBatchedHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnNewEntries_WhenEntriesArePresent()
    {
        //Arrange
        var repo = Substitute.For<ITimeSeriesRepository<TemperatureEntry>>();
        _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>().Returns(repo);

        _mapper.Map<TemperatureEntry>(Arg.Is(_testObjects.TemperatureEntryDtos[0])).Returns(_testObjects.TemperatureEntries[0]);
        _mapper.Map<TemperatureEntry>(Arg.Is(_testObjects.TemperatureEntryDtos[1])).Returns(_testObjects.TemperatureEntries[1]);

        //Act
        var result = await _sut.Handle(new AddTemperatureEntriesBatchedCommand(_testObjects.TemperatureEntryDtos), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.TemperatureEntryDtos);
        await repo.Received(1).AddRangeBatchedAsync(Arg.Is<List<TemperatureEntry>>(entries
            => entries.Count == _testObjects.TemperatureEntries.Count
        && entries.TrueForAll(item
            => _testObjects.TemperatureEntries.Contains(item))
        ));
    }
}