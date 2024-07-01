using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Queries;

public class GetCurrentFeatherDeviceRecipeQueryTest
{
    private readonly IDeviceUnitOfWork _deviceUnitOfWork = Substitute.For<IDeviceUnitOfWork>();
    private readonly IDeviceService<FeatherDeviceDto> _deviceService = Substitute.For<IDeviceService<FeatherDeviceDto>>();
    private readonly GetCurrentFeatherDeviceRecipeHandler _sut;
    private readonly FeatherDeviceFeatureTestObjects _testObjects = new();

    public GetCurrentFeatherDeviceRecipeQueryTest()
    {
        _sut = new(_deviceUnitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCallReadAsync_WhenDevicePresent()
    {
        //Arrange
        var dtoWithAction = _testObjects.FeatherDeviceDto;
        dtoWithAction.Recipe = _testObjects.XivotecRecipeDto;

        _deviceService.GetCurrentConfigAsync(Arg.Is(_testObjects.FeatherDeviceDto)).Returns(dtoWithAction);
        _deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>().Returns(_deviceService);

        //Act
        var result = await _sut.Handle(new GetCurrentFeatherDeviceRecipeQuery(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        result.Should().Be(dtoWithAction.Recipe);
    }
}