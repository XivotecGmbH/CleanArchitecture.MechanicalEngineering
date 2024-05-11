using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.RecipeFeature;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe;

public partial class RecipeControlViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<XivotecRecipeDto> _recipeCollection = [];

    [ObservableProperty]
    private ObservableCollection<string> _deviceRecipeNames = [];

    [ObservableProperty]
    private string _selectedItem = string.Empty;

    private readonly IMediator _mediator;
    private readonly IRecipeService _recipeService;

    public RecipeControlViewModel(
        INavigationService navigation,
        IMediator mediator,
        IRecipeService recipeService,
        ILogger<RecipeControlViewModel> logger)
        : base(navigation, logger)
    {
        _mediator = mediator;
        _recipeService = recipeService;
        AddUsedDeviceRecipes();
    }

    [RelayCommand]
    public async Task AddRecipe()
    {
        if (string.IsNullOrEmpty(SelectedItem))
        {
            return;
        }

        var selectedDeviceName = SelectedItem + "DetailViewModel";
        await Navigation.NavigateToAsync(selectedDeviceName);
    }

    [RelayCommand]
    public async Task ListTapped(XivotecRecipeDto xivotecRecipe)
    {
        await Navigation.NavigateToAsync(nameof(FeatherDeviceRecipeDetailViewModel), xivotecRecipe);
    }

    [RelayCommand]
    public async Task DeleteRecipe(XivotecRecipeDto xivotecRecipeDto)
    {
        await _recipeService.DeleteRecipeAsync(xivotecRecipeDto);
        await RefreshList();
    }

    public override async Task PageAppearing()
    {
        await RefreshList();
    }

    private async Task RefreshList()
    {
        var xivotecRecipes = await _mediator.Send(new GetXivotecRecipeAllQuery());
        RecipeCollection = new ObservableCollection<XivotecRecipeDto>(xivotecRecipes);
    }

    private void AddUsedDeviceRecipes()
    {
        foreach (var propertyInfo in typeof(XivotecRecipeDto).GetProperties())
        {
            var name = propertyInfo.Name;

            if (!name.Contains("Device"))
            {
                continue;
            }


            DeviceRecipeNames.Add(name);
        }

        if (DeviceRecipeNames.Count == 1)
        {
            SelectedItem = DeviceRecipeNames[0];
        }
    }
}
