using AutoMapper;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.Common.Recipe.Interface;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xivotec.CleanArchitecture.Infrastructure.Recipe.Interface;

namespace Xivotec.CleanArchitecture.Infrastructure.Recipe;

/// <inheritdoc cref="IRecipeService"/>
public class RecipeService : IRecipeService
{
    private readonly IRecipeImporter _importer;
    private readonly IRecipeExporter _exporter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecipeService(IRecipeImporter importer,
        IRecipeExporter exporter,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _importer = importer;
        _exporter = exporter;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<XivotecRecipeDto> ImportRecipeAsync(string path)
        => await _importer.ImportRecipeFromFileAsync(path);

    public async Task<List<XivotecRecipeDto>> ImportRecipesFromDirectoryAsync(string path)
        => await _importer.ImportRecipesFromDirectoryAsync(path);

    public async Task ExportRecipeAsync(string path, XivotecRecipeDto recipe)
        => await _exporter.ExportRecipeAsync(path, recipe);

    public async Task<XivotecRecipeDto> LoadRecipeAsync(Guid id)
    {
        var repo = _unitOfWork.GetRelationalRepository<XivotecRecipe>();
        var selectedEntity = await repo.GetByIdAsync(id);
        var recipeDto = _mapper.Map<XivotecRecipeDto>(selectedEntity);
        return recipeDto;
    }

    public async Task UpdateRecipeAsync(XivotecRecipeDto recipe)
    {
        var xivotecRecipeRepository = _unitOfWork.GetRelationalRepository<XivotecRecipe>();
        var featherDeviceRepository = _unitOfWork.GetRelationalRepository<FeatherDeviceRecipe>();
        var xivotecEntity = _mapper.Map<XivotecRecipe>(recipe);

        await xivotecRecipeRepository.UpdateAsync(xivotecEntity);
        await featherDeviceRepository.UpdateAsync(xivotecEntity.FeatherDeviceRecipe!);
    }

    public async Task SaveRecipeAsync(XivotecRecipeDto recipe)
    {
        var xivotecRecipeRepository = _unitOfWork.GetRelationalRepository<XivotecRecipe>();
        var xivotecEntity = _mapper.Map<XivotecRecipe>(recipe);

        await xivotecRecipeRepository.AddAsync(xivotecEntity);
    }

    public async Task DeleteRecipeAsync(XivotecRecipeDto recipe)
    {
        var xivotecRecipeRepository = _unitOfWork.GetRelationalRepository<XivotecRecipe>();
        var featherDeviceRepository = _unitOfWork.GetRelationalRepository<FeatherDeviceRecipe>();
        var xivotecEntity = _mapper.Map<XivotecRecipe>(recipe);

        await xivotecRecipeRepository.DeleteAsync(xivotecEntity);
        await featherDeviceRepository.DeleteAsync(xivotecEntity.FeatherDeviceRecipe!);
    }
}