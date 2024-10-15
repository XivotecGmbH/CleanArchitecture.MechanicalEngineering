using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Common;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Commands;

public class AddTemperatureEntryBatchedCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TemperatureFeatureTestObjects _testObjects = new();

    private readonly AddTemperatureEntryBatchedHandler _sut;

    public AddTemperatureEntryBatchedCommandTest()
    {
        _sut = new AddTemperatureEntryBatchedHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnNewEntry_WhenEntryIsPresent()
    {
        //Arrange
        var repo = Substitute.For<ITimeSeriesRepository<TemperatureEntry>>();
        _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>().Returns(repo);

        _mapper.Map<TemperatureEntry>(Arg.Is(_testObjects.TemperatureEntryDtos[0])).Returns(_testObjects.TemperatureEntries[0]);

        //Act
        var result = await _sut.Handle(new AddTemperatureEntryBatchedCommand(_testObjects.TemperatureEntryDtos[0]), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.TemperatureEntryDtos[0]);
        await repo.Received(1).AddBatchedAsync(Arg.Is(_testObjects.TemperatureEntries[0]));
    }
}