using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;
public class AddFeatherDeviceCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IRelationalRepository<FeatherDevice> _featherDeviceRepository = Substitute.For<IRelationalRepository<FeatherDevice>>();
    private readonly FeatherDeviceFeatureTestObjects _testObjects;
    private readonly AddFeatherDeviceHandler _sut;

    public AddFeatherDeviceCommandTest()
    {
        _sut = new(_mapper,
            _unitOfWork);

        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnNewFeatherDevice_WhenDevicePresent()
    {
        //Arrange
        _mapper.Map<FeatherDevice>(Arg.Is(_testObjects.FeatherDeviceDto)).Returns(_testObjects.FeatherDevices[0]);
        _mapper.Map<FeatherDeviceDto>(Arg.Is(_testObjects.FeatherDevices[0])).Returns(_testObjects.FeatherDeviceDto);
        _unitOfWork.GetRelationalRepository<FeatherDevice>().Returns(_featherDeviceRepository);

        //Act
        var result = await _sut.Handle(new AddFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.FeatherDeviceDto);

        await _featherDeviceRepository.Received(1).AddAsync(_testObjects.FeatherDevices[0]);
    }
}
