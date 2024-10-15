using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;
using Xivotec.CleanArchitecture.Domain.TemperatureAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.TemperatureFeature.Commands;

public class DeleteTemperatureEntryRangeCommandTest
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DateTime _deleteStartDate = DateTime.MinValue;
    private readonly DateTime _deleteEndDate = DateTime.MaxValue;

    private readonly DeleteTemperatureEntryRangeHandler _sut;

    public DeleteTemperatureEntryRangeCommandTest()
    {
        _sut = new DeleteTemperatureEntryRangeHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<ITimeSeriesRepository<TemperatureEntry>>();
        _unitOfWork.GetTimeSeriesRepository<TemperatureEntry>().Returns(repo);

        //Act
        await _sut.Handle(new DeleteTemperatureEntryRangeCommand(
                _deleteStartDate, _deleteEndDate),
            CancellationToken.None);

        //Assert
        await repo.Received(1).DeleteRangeAsync(Arg.Is(_deleteStartDate), Arg.Is(_deleteEndDate));
    }
}