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

public class GetXivotecRecipeAllQueryTests
{
    private readonly GetXivotecRecipeAllQueryHandler _sut;

    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly XivotecRecipeTestableObjects _testObjects;

    public GetXivotecRecipeAllQueryTests()
    {
        _sut = new(_mapper, _unitOfWork);
        _testObjects = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfXivotecRecipeDtos()
    {
        // Arrange
        var repository = Substitute.For<IRepository<XivotecRecipe>>();
        repository.GetAllAsync().Returns(_testObjects.XivotecRecipes);
        _unitOfWork.GetRepository<XivotecRecipe>().Returns(repository);

        _mapper.Map<XivotecRecipeDto>(Arg.Is(_testObjects.XivotecRecipes[0]))
            .Returns(_testObjects.XivotecRecipeDtos[0]);
        _mapper.Map<XivotecRecipeDto>(Arg.Is(_testObjects.XivotecRecipes[1]))
            .Returns(_testObjects.XivotecRecipeDtos[1]);

        // Act
        var result = await _sut.Handle(new GetXivotecRecipeAllQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_testObjects.XivotecRecipeDtos);
    }
}
