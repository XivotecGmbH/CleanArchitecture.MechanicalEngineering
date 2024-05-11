using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Services.PersistenceConfiguration;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Theme;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isLightMode;

    [ObservableProperty]
    private string _dbHost = string.Empty;

    [ObservableProperty]
    private string _dbPort = string.Empty;

    [ObservableProperty]
    private string _dbName = string.Empty;

    private readonly IPersistenceConfigurationService _configurationService;
    private readonly IThemeService _themeService;

    public SettingsViewModel(INavigationService navigation,
        ILogger<SettingsViewModel> logger,
        IPersistenceConfigurationService configurationService,
        IThemeService themeService)
        : base(navigation, logger)
    {
        _configurationService = configurationService;
        _themeService = themeService;
        _ = Initialize();
    }

    protected override async Task Initialize()
    {
        // get current settings for display only
        var configDto = _configurationService.GetPersistenceConfigurationDto();
        DbHost = configDto.Host ?? string.Empty;
        DbPort = configDto.Port ?? string.Empty;
        DbName = configDto.PersistenceName ?? string.Empty;

        await Task.CompletedTask;
    }

    partial void OnIsLightModeChanged(bool value)
    {
        _themeService.ToggleTheme(value);
    }
}