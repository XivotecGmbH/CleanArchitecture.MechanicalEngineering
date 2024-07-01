using FluentAssertions;
using System.Text.Json;
using Xivotec.CleanArchitecture.Infrastructure.Recipe;
using Xivotec.CleanArchitecture.Infrastructure.UnitTests.Devices.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Devices.Recipe;

public class RecipeImporterTests
{
    private readonly RecipeImporter _sut;

    private readonly XivotecRecipeTestableObjects _testableObjects;

    public RecipeImporterTests()
    {
        _sut = new();
        _testableObjects = new();
    }

    [Fact]
    public async Task ImportRecipeFromFileAsync_ShouldDeserializeJsonFileCorrectly()
    {
        // Arrange
        var json = JsonSerializer.Serialize(_testableObjects.XivotecRecipeDto);
        var tempFilePath = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFilePath, json);

        // Act
        var actualRecipe = await _sut.ImportRecipeFromFileAsync(tempFilePath);

        // Assert
        actualRecipe.Should().BeEquivalentTo(_testableObjects.XivotecRecipeDto);

        // Clean up
        File.Delete(tempFilePath);
    }
}
