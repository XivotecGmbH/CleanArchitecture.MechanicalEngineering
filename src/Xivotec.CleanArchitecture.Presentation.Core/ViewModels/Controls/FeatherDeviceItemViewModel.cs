using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Application.Services.Device;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;
using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

public partial class FeatherDeviceItemViewModel : ViewModelBase,
    IRecipient<DevicePageAppearingMessage>
{
    [ObservableProperty]
    private bool _isListDevicesFilled;

    [ObservableProperty]
    private ObservableCollection<FeatherDeviceDto> _devices = [];

    private readonly IMediator _mediator;
    private readonly IFeatherDeviceService _featherDeviceService;

    public FeatherDeviceItemViewModel(
        INavigationService navigation,
        ILogger<FeatherDeviceItemViewModel> logger,
        IFeatherDeviceService featherDeviceService,
        IMediator mediator)
        : base(navigation, logger)
    {
        _mediator = mediator;
        _featherDeviceService = featherDeviceService;

        Task.Run(RefreshList).GetAwaiter().GetResult();
    }

    [RelayCommand]
    public async Task EditList(FeatherDeviceDto featherDeviceDto)
    {
        await Navigation.NavigateToAsync(nameof(FeatherDeviceConfigViewModel), featherDeviceDto);
    }

    [RelayCommand]
    public async Task DeleteList(FeatherDeviceDto featherDeviceDto)
    {
        await _featherDeviceService.DeinitializeAsync(featherDeviceDto);
        await _mediator.Send(new DeleteFeatherDeviceCommand(featherDeviceDto));
        await RefreshList();
    }


    [RelayCommand]
    public async Task ListTapped(FeatherDeviceRecipeDto featherDeviceDto)
    {
        await Navigation.NavigateToAsync(nameof(FeatherDeviceRecipeDetailViewModel), featherDeviceDto);
    }

    public void Receive(DevicePageAppearingMessage message)
        => Task.Run(RefreshList).GetAwaiter().GetResult();


    private async Task RefreshList()
    {
        var foundDevices = await _mediator.Send(new GetFeatherDeviceAllQuery());

        Devices = new ObservableCollection<FeatherDeviceDto>(foundDevices);

        IsListDevicesFilled = Devices.Count > 0;
    }
}
