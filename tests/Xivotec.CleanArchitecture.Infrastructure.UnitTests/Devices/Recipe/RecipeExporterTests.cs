using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Text.Json;
using Xivotec.CleanArchitecture.Infrastructure.Recipe;
using Xivotec.CleanArchitecture.Infrastructure.UnitTests.Devices.Common;
using Xunit;

namespace Xivotec.CleanArchitecture.Infrastructure.UnitTests.Devices.Recipe;

public class RecipeExporterTests
{
    private readonly RecipeExporter _sut;

    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
    private readonly XivotecRecipeTestableObjects _testableObjects;

    public RecipeExporterTests()
    {
        _sut = new(_configuration);
        _testableObjects = new();
    }

    [Fact]
    public async Task ExportRecipeAsync_ShouldCreateJsonFileWithCorrectContent()
    {
        // Arrange
        var tempFilePath = Path.GetTempFileName();
        var tempFolderPath = Path.GetDirectoryName(tempFilePath);
        _configuration["RecipeBasePath"].Returns(tempFolderPath);
        var expectedFilePath = Path.Combine(
            tempFolderPath!,
            "TestRecipe" + ".json");

        // Act
        await _sut.ExportRecipeAsync("TestRecipe", _testableObjects.XivotecRecipeDto);

        // Assert
        File.Exists(expectedFilePath).Should().BeTrue();
        var actualContent = await File.ReadAllTextAsync(expectedFilePath);
        var expectedContent = JsonSerializer.Serialize(_testableObjects.XivotecRecipeDto);

        actualContent.Should().Be(expectedContent);

        // Clean up
        File.Delete(expectedFilePath);
    }
}
