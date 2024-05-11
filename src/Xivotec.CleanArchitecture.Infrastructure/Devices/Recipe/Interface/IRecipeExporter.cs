using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;

namespace Xivotec.CleanArchitecture.Infrastructure.Devices.Recipe.Interface;

/// <summary>
/// Service for exporting <see cref="XivotecRecipeDto"/>.
/// </summary>
public interface IRecipeExporter
{
    /// <summary>
    /// Export a <see cref="XivotecRecipeDto"/>
    /// </summary>
    /// <param name="name">The name of the Recipe.</param>
    /// <param name="recipe">The recipe to be exported.</param>
    /// <returns>The completed task.</returns>
    public Task ExportRecipeAsync(string name, XivotecRecipeDto recipe);
}