using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.RecipeFeature;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Infrastructure.Exceptions;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

public partial class FeatherDeviceRecipeDetailViewModel : ViewModelBase,
    IRecipient<FilePathApprovedMessage>
{
    [ObservableProperty]
    private XivotecRecipeDto _selectedRecipe = new();

    [ObservableProperty]
    private string _recipeNameEntryValue = string.Empty;

    [ObservableProperty]
    private string _intervalEntryValue = string.Empty;

    [ObservableProperty]
    private string _selectedLedColor = string.Empty;

    [ObservableProperty]
    private bool _isLedSwitchedOnValue;

    [ObservableProperty]
    private bool _isDisplaySwitchedOnValue;

    [ObservableProperty]
    private List<string> _colors = [];

    [ObservableProperty]
    private string _recipeInputErrorLabel = string.Empty;

    private bool _isRecipeNew = true;


    private readonly IRecipeService _featherDeviceRecipeService;

    public FeatherDeviceRecipeDetailViewModel(
        IRecipeService recipeService,
        ILogger<FeatherDeviceRecipeDetailViewModel> logger,
        INavigationService navigation)
        : base(navigation, logger)
    {
        _featherDeviceRecipeService = recipeService;

        AddAvailableColors();
    }

    [RelayCommand]
    public async Task NavigateBack()
    {
        ResetInputEntries();
        await Navigation.NavigateBackAsync();
    }

    [RelayCommand]
    public async Task ImportRecipeAsync()
        => await Navigation.NavigateToAsync(nameof(FeatherDeviceRecipeImportViewModel));

    [RelayCommand]
    public async Task SaveRecipe()
    {
        var recipeDto = CreateRecipeDto();

        if (recipeDto is null)
        {
            return;
        }

        if (_isRecipeNew)
        {
            await _featherDeviceRecipeService.SaveRecipeAsync(recipeDto);
        }
        else
        {
            await _featherDeviceRecipeService.UpdateRecipeAsync(recipeDto);
        }

        ResetInputEntries();

        await Navigation.NavigateToAsync(nameof(RecipeControlViewModel));
    }

    [RelayCommand]
    public async Task ExportRecipe()
    {
        var recipeDto = CreateRecipeDto();

        if (recipeDto is null)
        {
            return;
        }

        await _featherDeviceRecipeService.ExportRecipeAsync(RecipeNameEntryValue, recipeDto);

        ResetInputEntries();

        await Navigation.NavigateToAsync(nameof(RecipeControlViewModel));
    }

    public void Receive(FilePathApprovedMessage message) => Task.Run(async () => await ImportRecipeAsync(message.Value));

    protected override async Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        SelectedRecipe = (XivotecRecipeDto)dto.Value;

        if (SelectedRecipe.FeatherDeviceRecipe is null)
        {
            return;
        }

        RecipeNameEntryValue = SelectedRecipe.FeatherDeviceRecipe.Name;

        UpdateRecipeEntries();

        _isRecipeNew = false;

        await Task.CompletedTask;
    }

    private async Task ImportRecipeAsync(string path)
    {
        var recipe = new XivotecRecipeDto();
        try
        {
            recipe = await _featherDeviceRecipeService.ImportRecipeAsync(path);
        }

        catch (RecipeImportException)
        {
            RecipeInputErrorLabel = "Error during import of recipe";
            return;
        }


        SelectedRecipe = recipe;

        var fileName = Path.GetFileNameWithoutExtension(path);
        RecipeNameEntryValue = fileName;

        _isRecipeNew = true;

        UpdateRecipeEntries();
    }

    private void UpdateRecipeEntries()
    {
        if (SelectedRecipe.FeatherDeviceRecipe is null)
        {
            return;
        }

        IntervalEntryValue = SelectedRecipe.FeatherDeviceRecipe.Interval.ToString();
        SelectedLedColor = SelectedRecipe.FeatherDeviceRecipe.LedColor.ToString();
        IsLedSwitchedOnValue = SelectedRecipe.FeatherDeviceRecipe.IsLedSwitchedOn;
        IsDisplaySwitchedOnValue = SelectedRecipe.FeatherDeviceRecipe.IsDisplaySwitchedOn;
    }

    private XivotecRecipeDto? CreateRecipeDto()
    {
        var isValid = IsInputValid();

        if (!isValid)
        {
            RecipeInputErrorLabel = "Wrong parameters for recipe";
            return null;
        }

        var featherDeviceRecipeDto = new FeatherDeviceRecipeDto()
        {
            Id = _isRecipeNew ? Guid.NewGuid() : SelectedRecipe.FeatherDeviceRecipe!.Id,
            Name = RecipeNameEntryValue,
            Interval = int.Parse(IntervalEntryValue),
            LedColor = Enum.Parse<LedColorDto>(SelectedLedColor),
            IsLedSwitchedOn = IsLedSwitchedOnValue,
            IsDisplaySwitchedOn = IsDisplaySwitchedOnValue
        };

        var recipeDto = new XivotecRecipeDto()
        {
            Id = _isRecipeNew ? Guid.NewGuid() : SelectedRecipe.Id,
            Name = "Xivotec Recipe : " + RecipeNameEntryValue,
            FeatherDeviceRecipe = featherDeviceRecipeDto,
        };

        return recipeDto;
    }

    private void ResetInputEntries()
    {
        RecipeNameEntryValue = "";
        IntervalEntryValue = "";
        SelectedLedColor = "";
        IsLedSwitchedOnValue = false;
        IsDisplaySwitchedOnValue = false;

        _isRecipeNew = true;

        RecipeInputErrorLabel = string.Empty;
    }

    private bool IsInputValid()
    {
        return !string.IsNullOrWhiteSpace(IntervalEntryValue) && !string.IsNullOrWhiteSpace(RecipeNameEntryValue) &&
            IntervalEntryValue.All(char.IsDigit);
    }

    private void AddAvailableColors()
    {
        foreach (var color in Enum.GetValues<LedColorDto>())
        {
            Colors.Add(color.ToString());
        }
    }
}
