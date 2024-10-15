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

public class GetTemperatureEntriesBySourceInRangeQueryTest
{
    private const string Source = "1";

    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TemperatureFeatureTestObjects _testObjects = new();
    private readonly DateTime _getStartDate = DateTime.MinValue;
    private readonly DateTime _getEndDate = DateTime.MaxValue;

    private readonly GetTemperatureEntriesBySourceInRangeQueryHandler _sut;

    public GetTemperatureEntriesBySourceInRangeQueryTest()
    {
        _sut = new GetTemperatureEntriesBySourceInRangeQueryHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectEntries_WhenEntriesExist()
    {
        //Arrange
        var repo = Substitute.For<ITimeSeriesRepository<TemperatureEntry>>();
        _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>().Returns(repo);
        var listToReturn = new List<TemperatureEntry>
        {
            _testObjects.TemperatureEntries[0]
        };
        repo.GetBySourceInRange(Arg.Is(_getStartDate), Arg.Is(_getEndDate), Arg.Is(Source)).Returns(listToReturn);

        _mapper.Map<TemperatureEntryDto>(Arg.Is(_testObjects.TemperatureEntries[0])).Returns(_testObjects.TemperatureEntryDtos[0]);

        //Act
        var result = await _sut.Handle(new GetTemperatureEntriesBySourceInRangeQuery(_getStartDate, _getEndDate, Source), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(listToReturn.Count);
        result.Should().BeEquivalentTo(listToReturn);
    }
}