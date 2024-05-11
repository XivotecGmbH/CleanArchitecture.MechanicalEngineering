using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly ILogger<MainViewModel> _logger;

    public MainViewModel(
        INavigationService navigation,
        ILogger<MainViewModel> logger)
        : base(navigation, logger)
    {
        _logger = logger;
        _logger.LogInformation("Main");
    }
}