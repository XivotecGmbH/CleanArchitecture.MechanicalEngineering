using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Queries;

public class GetConnectionStateFeatherDeviceQueryTest
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork = Substitute.For<IDeviceUnitOfWork>();
    private readonly IDeviceService<FeatherDeviceDto> _deviceService = Substitute.For<IDeviceService<FeatherDeviceDto>>();
    private readonly GetConnectionStateFeatherDeviceHandler _sut;
    private readonly FeatherDeviceFeatureTestObjects _testObjects = new();

    public GetConnectionStateFeatherDeviceQueryTest()
    {
        _sut = new(_deviceUnitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCallReadAsync_WhenDevicePresent()
    {
        //Arrange
        var dtoWithAction = _testObjects.FeatherDeviceDto;
        _deviceService.ReadDataAsync(Arg.Is(dtoWithAction)).Returns(dtoWithAction);
        _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>().Returns(_deviceService);

        //Act
        var result = await _sut.Handle(new GetConnectionStateFeatherDeviceQuery(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        result.Should().Be(_testObjects.FeatherDeviceDto.ConnectionState);
    }
}