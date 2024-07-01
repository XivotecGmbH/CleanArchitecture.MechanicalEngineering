using System.Text.Json;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Exceptions;
using Xivotec.CleanArchitecture.Infrastructure.Recipe.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.Recipe;

/// <inheritdoc cref="IRecipeImporter"/>
public class RecipeImporter : IRecipeImporter
{
    public async Task<XivotecRecipeDto> ImportRecipeFromFileAsync(string path)
    {
        await using var fileStream = File.OpenRead(path);

        var recipeDto = new XivotecRecipeDto();

        try
        {
            recipeDto = await JsonSerializer.DeserializeAsync<XivotecRecipeDto>(fileStream);
        }
        catch (JsonException ex)
        {
            throw new RecipeImportException(ex.Message);
        }

        return recipeDto!;
    }

    public async Task<List<XivotecRecipeDto>> ImportRecipesFromDirectoryAsync(string path)
    {
        var recipeDtos = new List<XivotecRecipeDto>();

        foreach (var file in Directory.EnumerateFiles(path, "*.json"))
        {
            var recipeDto = await ImportRecipeFromFileAsync(file);
            recipeDtos.Add(recipeDto);
        }

        return recipeDtos;
    }
}