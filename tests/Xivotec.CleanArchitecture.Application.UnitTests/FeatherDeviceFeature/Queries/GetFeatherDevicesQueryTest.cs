using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Common;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherDeviceFeature.Queries;
public class GetFeatherDevicesQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly FeatherDeviceFeatureTestObjects _testObjects;

    private readonly GetFeatherDeviceAllQueryHandler _sut;

    public GetFeatherDevicesQueryTest()
    {
        _sut = new GetFeatherDeviceAllQueryHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldFindAllDevices_WhenDevicesExists()
    {
        //Arrange
        var repo = _unitOfWork.GetRelationalRepository<FeatherDevice>();
        repo.GetAllAsync().Returns(_testObjects.FeatherDevices);

        _mapper.Map<FeatherDeviceDto>(Arg.Is(_testObjects.FeatherDevices[0])).Returns(_testObjects.FeatherDeviceDto);
        _mapper.Map<FeatherDeviceDto>(Arg.Is(_testObjects.FeatherDevices[1])).Returns(_testObjects.FeatherDeviceDto);

        //Act
        var result = await _sut.Handle(new GetFeatherDeviceAllQuery(), CancellationToken.None);

        //Assert
        result.Count.Should().Be(2);
        result[0].Should().BeEquivalentTo(_testObjects.FeatherDeviceDto);
        result[1].Should().BeEquivalentTo(_testObjects.FeatherDeviceDto);
    }
}
