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
public class GetFeatherDeviceByIdQueryTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly FeatherDeviceFeatureTestObjects _testObjects;

    private readonly GetFeatherDeviceByIdQueryHandler _sut;

    public GetFeatherDeviceByIdQueryTest()
    {
        _sut = new GetFeatherDeviceByIdQueryHandler(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldFindCorrectDeviceById_WhenDeviceExists()
    {
        //Arrange
        var repo = _unitOfWork.GetRepository<FeatherDevice>();
        repo.GetByIdAsync(Arg.Is(_testObjects.FeatherDevices[0].Id)).Returns(_testObjects.FeatherDevices[0]);

        _mapper.Map<FeatherDeviceDto>(Arg.Is(_testObjects.FeatherDevices[0])).Returns(_testObjects.FeatherDeviceDto);

        //Act
        var result = await _sut.Handle(new GetFeatherDeviceByIdQuery(_testObjects.FeatherDevices[0].Id), CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(_testObjects.FeatherDeviceDto.Id);
    }
}
