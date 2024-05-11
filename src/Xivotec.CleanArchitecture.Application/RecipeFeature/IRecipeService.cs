using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.RecipeFeature;

/// <summary>
/// Service for managing recipes.
/// </summary>
public interface IRecipeService
{
    /// <summary>
    /// Import a recipe from a JSON file.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <returns>The imported <see cref="XivotecRecipeDto"/>.</returns>
    /// <exception cref="RecipeImportException"> Exception during import of a recipe.</exception>
    public Task<XivotecRecipeDto> ImportRecipeAsync(string path);

    /// <summary>
    /// Import a list of recipes from a folder path.
    /// </summary>
    /// <param name="path">The folder path containing the recipes.</param>
    /// <returns>Imported <see cref="XivotecRecipeDto"/> instances.</returns>
    /// <exception cref="RecipeImportException"> Exception during import of a recipe.</exception>
    public Task<List<XivotecRecipeDto>> ImportRecipesFromDirectoryAsync(string path);

    /// <summary>
    /// Export a <see cref="XivotecRecipeDto"/> as a JSON file.
    /// </summary>
    /// <param name="path">The path of the JSON file.</param>
    /// <param name="recipe">The <see cref="XivotecRecipeDto"/> to be exported.</param>
    /// <returns>The completed task.</returns>
    public Task ExportRecipeAsync(string path, XivotecRecipeDto recipe);

    /// <summary>
    /// Load the specified <see cref="XivotecRecipeDto"/> from the repository.
    /// </summary>
    /// <param name="id">The GUID of the recipe to be loaded.</param>
    /// <returns>The specified <see cref="XivotecRecipeDto"/></returns>
    public Task<XivotecRecipeDto> LoadRecipeAsync(Guid id);

    /// <summary>
    /// Update a <see cref="XivotecRecipeDto"/>.
    /// </summary>
    /// <param name="recipe">The recipe to be updated.</param>
    public Task UpdateRecipeAsync(XivotecRecipeDto recipe);

    /// <summary>
    /// Save a <see cref="XivotecRecipeDto"/> to the repository.
    /// </summary>
    /// <param name="recipe">The recipe to be saved.</param>
    public Task SaveRecipeAsync(XivotecRecipeDto recipe);

    /// <summary>
    /// Deletes a <see cref="XivotecRecipeDto"/> from the repository.
    /// </summary>
    /// <param name="recipe">The recipe to be deleted.</param>
    public Task DeleteRecipeAsync(XivotecRecipeDto recipe);
}