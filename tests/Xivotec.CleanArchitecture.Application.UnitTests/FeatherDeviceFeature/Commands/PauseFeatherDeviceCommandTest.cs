﻿using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;

public class PauseFeatherDeviceCommandTest
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork = Substitute.For<IDeviceUnitOfWork>();
    private readonly IDeviceService<FeatherDeviceDto> _deviceService = Substitute.For<IDeviceService<FeatherDeviceDto>>();
    private readonly PauseFeatherDeviceHandler _sut;
    private readonly FeatherDeviceFeatureTestObjects _testObjects = new();

    public PauseFeatherDeviceCommandTest()
        => _sut = new(_deviceUnitOfWork);

    [Fact]
    public async Task Handle_ShouldCallWriteAsync_WhenDevicePresent()
    {
        //Arrange
        var dtoWithAction = _testObjects.FeatherDeviceDto;
        dtoWithAction.Action = FeatherDeviceActionsDto.Pause;
        _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>().Returns(_deviceService);

        //Act
        await _sut.Handle(new PauseFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        await _deviceService.Received(1).WriteDataAsync(Arg.Is(dtoWithAction));
    }
}