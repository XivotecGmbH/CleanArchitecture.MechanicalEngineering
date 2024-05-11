using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.FileManagement;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

public partial class FeatherDeviceRecipeImportViewModel : ViewModelBase
{
    private readonly IFileSelectionService _fileSelectionService;

    [ObservableProperty]
    private string _recipeIdEntryValue = string.Empty;

    [ObservableProperty]
    private string _recipePathValue = string.Empty;

    [ObservableProperty]
    private string _importErrorLabel = string.Empty;

    [ObservableProperty]
    private bool _recipeFilepathLabelVisibility;

    public FeatherDeviceRecipeImportViewModel(
        INavigationService navigation,
        ILogger<FeatherDeviceRecipeImportViewModel> logger,
        IFileSelectionService fileSelectionService)
        : base(navigation, logger)
    {
        _fileSelectionService = fileSelectionService;
    }

    [RelayCommand]
    public async Task CancelRecipeImportAsync() => await NavigateBackAsync();

    [RelayCommand]
    public async Task ApplyImportAsync()
    {
        WeakReferenceMessenger.Default.Send(new FilePathApprovedMessage(RecipePathValue));
        await NavigateBackAsync();
    }

    [RelayCommand]
    public async Task ImportRecipe()
    {
        var path = await _fileSelectionService.SelectFilePath();

        if (path.EndsWith("json", StringComparison.OrdinalIgnoreCase))
        {
            RecipePathValue = path;
            RecipeFilepathLabelVisibility = true;
        }
        else
        {
            ImportErrorLabel = "You have to select a json file";
        }
    }
}
