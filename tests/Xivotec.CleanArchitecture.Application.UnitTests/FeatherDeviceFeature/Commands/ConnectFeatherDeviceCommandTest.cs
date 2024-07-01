using MediatR;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;

public class ConnectFeatherDeviceCommandTest
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork = Substitute.For<IDeviceUnitOfWork>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IDeviceService<FeatherDeviceDto> _deviceService = Substitute.For<IDeviceService<FeatherDeviceDto>>();
    private readonly ConnectFeatherDeviceHandler _sut;

    private readonly FeatherDeviceFeatureTestObjects _testObjects = new();

    public ConnectFeatherDeviceCommandTest()
        => _sut = new(_deviceUnitOfWork, _mediator);

    [Fact]
    public async Task Handle_ShouldCallConnectAsync_WhenDevicePresent()
    {
        //Arrange
        _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>().Returns(_deviceService);

        //Act
        await _sut.Handle(new ConnectFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        await _deviceService.Received(1).ConnectAsync(Arg.Is(_testObjects.FeatherDeviceDto));
    }
}