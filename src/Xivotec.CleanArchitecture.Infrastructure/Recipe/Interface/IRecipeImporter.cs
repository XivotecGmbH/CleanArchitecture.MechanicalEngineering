using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Exceptions;

namespace Xivotec.CleanArchitecture.Infrastructure.Recipe.Interface;

/// <summary>
/// Service for importing <see cref="XivotecRecipeDto"/>.
/// </summary>
public interface IRecipeImporter
{
    /// <summary>
    /// Import a <see cref="XivotecRecipeDto"/>
    /// </summary>
    /// <param name="path">The path of the Recipe.</param>
    /// <returns>The imported <see cref="XivotecRecipeDto"/>.</returns>
    /// <exception cref="RecipeImportException"> Error during import of a recipe</exception>
    public Task<XivotecRecipeDto> ImportRecipeFromFileAsync(string path);

    /// <summary>
    /// Import all recipes from a given folder.
    /// </summary>
    /// <param name="path">The folder path containing the recipes.</param>
    /// <returns>The imported <see cref="XivotecRecipeDto"/> instances.</returns>
    /// <exception cref="RecipeImportException"> Error during import of a recipe</exception>
    public Task<List<XivotecRecipeDto>> ImportRecipesFromDirectoryAsync(string path);
}