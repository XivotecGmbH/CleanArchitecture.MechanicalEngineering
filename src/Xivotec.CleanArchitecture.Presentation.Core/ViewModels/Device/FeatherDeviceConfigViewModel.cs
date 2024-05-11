using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Drawing;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.Services.Device;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

public partial class FeatherDeviceConfigViewModel : ViewModelBase
{
    private const int MaxValueOfPortNumberDigits = 3;
    private const string Com = "COM";


    [ObservableProperty]
    private FeatherDeviceDto _featherDevice = new();

    [ObservableProperty]
    private string _deviceName = string.Empty;

    [ObservableProperty]
    private string _comPortEntryValue = string.Empty;

    [ObservableProperty]
    private bool _isErrorLabelPortActive;

    [ObservableProperty]
    private Color _borderColorPort = new();

    [ObservableProperty]
    private bool _isErrorLabelDeviceNameActive;

    [ObservableProperty]
    private Color _borderColorDeviceName = new();

    private readonly IFeatherDeviceService _featherDeviceService;
    private readonly IMediator _mediator;

    public FeatherDeviceConfigViewModel(
        INavigationService navigation,
        ILogger<FeatherDeviceConfigViewModel> logger,
        IFeatherDeviceService featherDeviceService,
        IMediator mediator)
        : base(navigation, logger)
    {
        _featherDeviceService = featherDeviceService;
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task ApplyChanges()
    {
        IsErrorLabelDeviceNameActive = false;
        BorderColorDeviceName = Color.Transparent;

        IsErrorLabelPortActive = false;
        BorderColorPort = Color.Transparent;

        var isDeviceNameEmpty = string.IsNullOrEmpty(DeviceName);
        if (isDeviceNameEmpty)
        {
            IsErrorLabelDeviceNameActive = true;
            BorderColorDeviceName = Color.Red;
        }

        var isEntryValid = ValidateComPortValue();

        if (!isEntryValid)
        {
            IsErrorLabelPortActive = true;
            BorderColorPort = Color.Red;
        }

        if (isDeviceNameEmpty || !isEntryValid)
        {
            return;
        }

        await ApplyDeviceConfig();

        await Navigation.NavigateBackAsync();
    }

    protected override Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        FeatherDevice = (FeatherDeviceDto)dto.Value;
        return Task.CompletedTask;
    }

    private async Task ApplyDeviceConfig()
    {
        if (FeatherDevice.Id == Guid.Empty)
        {
            var deviceDto = new FeatherDeviceDto
            {
                Id = Guid.NewGuid(),
                Name = DeviceName,
                ComPort = Com + ComPortEntryValue,
                AvailableLedColors = Enum.GetValues<LedColorDto>().ToList()
            };

            await _mediator.Send(new AddFeatherDeviceCommand(deviceDto));

            await _featherDeviceService.InitialiseAsync(deviceDto);
        }
        else
        {
            await _featherDeviceService.DeinitializeAsync(FeatherDevice);

            var deviceDto = new FeatherDeviceDto
            {
                Id = FeatherDevice.Id,
                Name = DeviceName,
                ComPort = Com + ComPortEntryValue,
                AvailableLedColors = Enum.GetValues<LedColorDto>().ToList()
            };

            await _mediator.Send(new UpdateFeatherDeviceCommand(deviceDto));

            await _featherDeviceService.InitialiseAsync(deviceDto);
        }
    }

    private bool ValidateComPortValue()
    {
        return !string.IsNullOrEmpty(ComPortEntryValue) && ComPortEntryValue.All(char.IsDigit) && ComPortEntryValue.Length <= MaxValueOfPortNumberDigits;
    }
}
