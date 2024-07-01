using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;

public class DeinitializeFeatherDeviceCommandTest
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork = Substitute.For<IDeviceUnitOfWork>();
    private readonly IDeviceService<FeatherDeviceDto> _deviceService = Substitute.For<IDeviceService<FeatherDeviceDto>>();
    private readonly DeinitializeFeatherDeviceHandler _sut;
    private readonly FeatherDeviceFeatureTestObjects _testObjects = new();

    public DeinitializeFeatherDeviceCommandTest()
        => _sut = new(_deviceUnitOfWork);

    [Fact]
    public async Task Handle_ShouldCallDeinitializeAsync_WhenDevicePresent()
    {
        //Arrange
        _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>().Returns(_deviceService);

        //Act
        await _sut.Handle(new DeinitializeFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        await _deviceService.Received(1).DeinitializeAsync(Arg.Is(_testObjects.FeatherDeviceDto));
    }
}