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

public class GetTemperatureEntriesBySourceQueryTest
{
    private const string Source = "1";

    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TemperatureFeatureTestObjects _testObjects = new();

    private readonly GetTemperatureEntriesBySourceQueryHandler _sut;

    public GetTemperatureEntriesBySourceQueryTest()
    {
        _sut = new GetTemperatureEntriesBySourceQueryHandler(_unitOfWork, _mapper);
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
        repo.GetBySource(Arg.Is(Source)).Returns(listToReturn);

        _mapper.Map<TemperatureEntryDto>(Arg.Is(_testObjects.TemperatureEntries[0])).Returns(_testObjects.TemperatureEntryDtos[0]);

        //Act
        var result = await _sut.Handle(new GetTemperatureEntriesBySourceQuery(Source), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(listToReturn.Count);
        result.Should().BeEquivalentTo(listToReturn);
    }
}