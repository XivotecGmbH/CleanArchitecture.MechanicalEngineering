﻿using AutoMapper;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;
public class UpdateFeatherDeviceCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IRelationalRepository<FeatherDevice> _featherDeviceRepository = Substitute.For<IRelationalRepository<FeatherDevice>>();
    private readonly FeatherDeviceFeatureTestObjects _testObjects;
    private readonly UpdateFeatherDeviceHandler _sut;

    public UpdateFeatherDeviceCommandTest()
    {
        _sut = new UpdateFeatherDeviceHandler(_mapper,
            _unitOfWork);

        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange

        _mapper.Map<FeatherDevice>(Arg.Is(_testObjects.FeatherDeviceDto)).Returns(_testObjects.FeatherDevices[0]);
        _unitOfWork.GetRelationalRepository<FeatherDevice>().Returns(_featherDeviceRepository);

        //Act
        await _sut.Handle(new UpdateFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        await _featherDeviceRepository.Received(1).UpdateAsync(Arg.Is(_testObjects.FeatherDevices[0]));
    }
}
