using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;
using Xivotec.CleanArchitecture.Application.Processes.FeatherDeviceProcess.ProcessDefinitions;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Processes;

public partial class ProcessViewModel : ViewModelBase
{
    private readonly IProcessRunner _processRunner;

    public ProcessViewModel(INavigationService navigation,
        ILogger<ProcessViewModel> logger,
        IProcessRunner processRunner)
        : base(navigation, logger)
    {
        _processRunner = processRunner;
    }

    [RelayCommand]
    public async Task RunTestProcess()
        => await _processRunner.ExecuteProcessDefinitionAsync<FeatherDeviceMachineProcess>();
}
