using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Common;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Queries;

public class GetTemperatureEntriesRangeQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TemperatureFeatureTestObjects _testObjects = new();
    private readonly DateTime _getStartDate = DateTime.MinValue;
    private readonly DateTime _getEndDate = DateTime.MaxValue;

    private readonly GetTemperatureEntriesRangeQueryHandler _sut;

    public GetTemperatureEntriesRangeQueryTest()
    {
        _sut = new GetTemperatureEntriesRangeQueryHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectEntries_WhenEntriesExist()
    {
        //Arrange
        var repo = Substitute.For<ITimeSeriesRepository<TemperatureEntry>>();
        _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>().Returns(repo);
        repo.GetInRangeAsync(Arg.Is(_getStartDate), Arg.Is(_getEndDate)).Returns(_testObjects.TemperatureEntries);

        _mapper.Map<TemperatureEntryDto>(Arg.Is(_testObjects.TemperatureEntries[0])).Returns(_testObjects.TemperatureEntryDtos[0]);
        _mapper.Map<TemperatureEntryDto>(Arg.Is(_testObjects.TemperatureEntries[1])).Returns(_testObjects.TemperatureEntryDtos[1]);

        //Act
        var result = await _sut.Handle(new GetTemperatureEntriesRangeQuery(_getStartDate, _getEndDate), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(_testObjects.TemperatureEntryDtos.Count);
        result.Should().BeEquivalentTo(_testObjects.TemperatureEntryDtos);
    }
}