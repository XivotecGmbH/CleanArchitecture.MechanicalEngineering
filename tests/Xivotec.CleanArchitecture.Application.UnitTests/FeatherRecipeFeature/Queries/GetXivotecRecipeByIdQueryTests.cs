using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Queries;
using Xivotec.CleanArchitecture.Application.UnitTests.FeatherRecipeFeature.Common;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xunit;

namespace Xivotec.CleanArchitecture.Application.UnitTests.FeatherRecipeFeature.Queries;

public class GetXivotecRecipeByIdQueryTests
{
    private readonly GetXivotecRecipeByIdQueryHandler _sut;

    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly XivotecRecipeTestableObjects _testObjects;

    public GetXivotecRecipeByIdQueryTests()
    {
        _sut = new(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnXivotecRecipeDtoById()
    {
        // Arrange
        var repository = Substitute.For<IRelationalRepository<XivotecRecipe>>();
        repository.GetByIdAsync(_testObjects.XivotecRecipeDto.Id).Returns(_testObjects.XivotecRecipe);
        _unitOfWork.GetRelationalRepository<XivotecRecipe>().Returns(repository);

        _mapper.Map<XivotecRecipeDto>(Arg.Is(_testObjects.XivotecRecipe))
            .Returns(_testObjects.XivotecRecipeDto);

        // Act
        var result = await _sut.Handle(new GetXivotecRecipeByIdQuery(_testObjects.XivotecRecipeDto.Id), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.XivotecRecipeDto);
    }
}
