using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Infrastructure.Recipe.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.Recipe;

/// <inheritdoc cref="IRecipeExporter"/>
public class RecipeExporter : IRecipeExporter
{
    private readonly IConfiguration _configuration;

    public RecipeExporter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ExportRecipeAsync(string name, XivotecRecipeDto recipe)
    {
        var pathFromConfig = _configuration["RecipeBasePath"];
        var combinedPath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile),
            pathFromConfig!,
            name + ".json");

        await using var fileStream = File.Create(combinedPath);
        await JsonSerializer.SerializeAsync(fileStream, recipe);
    }
}