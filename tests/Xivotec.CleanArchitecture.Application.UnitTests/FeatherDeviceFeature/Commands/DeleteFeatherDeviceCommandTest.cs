﻿using AutoMapper;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Commands;
public class DeleteFeatherDeviceCommandTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly FeatherDeviceFeatureTestObjects _testObjects;

    private readonly DeleteFeatherDeviceHandler _sut;

    public DeleteFeatherDeviceCommandTest()
    {
        _sut = new DeleteFeatherDeviceHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldRun_WhenRequestIsValid()
    {
        //Arrange
        var repo = Substitute.For<IRepository<FeatherDevice>>();

        _mapper.Map<FeatherDevice>(Arg.Is(_testObjects.FeatherDeviceDto)).Returns(_testObjects.FeatherDevices[0]);
        _unitOfWork.GetRepository<FeatherDevice>().Returns(repo);

        //Act
        await _sut.Handle(new DeleteFeatherDeviceCommand(_testObjects.FeatherDeviceDto), CancellationToken.None);

        //Assert
        await repo.Received(1).DeleteAsync(Arg.Is(_testObjects.FeatherDevices[0]));
    }
}