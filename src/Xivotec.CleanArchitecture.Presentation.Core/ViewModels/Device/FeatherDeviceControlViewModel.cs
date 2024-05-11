using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using System.Drawing;
using Xivotec.CleanArchitecture.Application.Common.Device;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.Services.Device;
using Xivotec.CleanArchitecture.Infrastructure.Messages.ConnectionState;
using Xivotec.CleanArchitecture.Infrastructure.Messages.Data;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

public partial class FeatherDeviceControlViewModel : ViewModelBase,
    IRecipient<ConnectionStateChangedMessage>,
    IRecipient<DistanceDataChangedMessage>,
    IRecipient<TemperatureDataChangedMessage>,
    IRecipient<LumenDataChangedMessage>
{
    private const string NotSelected = "Not selected";

    [ObservableProperty]
    private FeatherDeviceDto _featherDevice = new();

    [ObservableProperty]
    private bool _isTempDataSelected = true;

    [ObservableProperty]
    private bool _isDistanceDataSelected = true;

    [ObservableProperty]
    private bool _isLumenDataSelected = true;

    [ObservableProperty]
    private bool _isDeviceConnected;

    [ObservableProperty]
    private string _connectionState = string.Empty;

    [ObservableProperty]
    private Color _connectionStateColor;

    [ObservableProperty]
    private string _temperatureValue = string.Empty;

    [ObservableProperty]
    private string _distanceValue = string.Empty;

    [ObservableProperty]
    private string _lumenValue = string.Empty;

    private readonly IFeatherDeviceService _featherDeviceService;

    public FeatherDeviceControlViewModel(
        INavigationService navigation,
        IFeatherDeviceService featherDeviceService,
        ILogger<FeatherDeviceControlViewModel> logger)
        : base(navigation, logger)
    {
        _featherDeviceService = featherDeviceService;
    }

    [RelayCommand]
    public async Task ConnectAsync() => await _featherDeviceService.ConnectAsync(FeatherDevice);

    [RelayCommand]
    public async Task DisconnectAsync() => await _featherDeviceService.DisconnectAsync(FeatherDevice);

    [RelayCommand]
    public async Task StartAsync()
    {
        await _featherDeviceService.StartAsync(FeatherDevice);
        await StartTemperatureDataStream();
        await StartDistanceDataStream();
        await StartLumenDataStream();
    }

    [RelayCommand]
    public async Task StopAsync() => await _featherDeviceService.StopAsync(FeatherDevice);

    [RelayCommand]
    public async Task PauseAsync() => await _featherDeviceService.PauseAsync(FeatherDevice);

    [RelayCommand]
    public async Task ContinueAsync() => await _featherDeviceService.ContinueAsync(FeatherDevice);


    public void Receive(ConnectionStateChangedMessage message) => UpdateConnectionState(message.Value ? ConnectionStateDto.Connected : ConnectionStateDto.Disconnected);

    public void Receive(TemperatureDataChangedMessage message) => TemperatureValue = message.Value;

    public void Receive(DistanceDataChangedMessage message) => DistanceValue = message.Value;

    public void Receive(LumenDataChangedMessage message) => LumenValue = message.Value;

    protected override async Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        FeatherDevice = (FeatherDeviceDto)dto.Value;

        var state = await _featherDeviceService.GetConnectionStateAsync(FeatherDevice);
        UpdateConnectionState(state);
    }

    private void UpdateConnectionState(ConnectionStateDto stateDto)
    {
        IsDeviceConnected = stateDto == ConnectionStateDto.Connected;

        ConnectionStateColor = stateDto == ConnectionStateDto.Connected ? Color.Green : Color.Red;

        ConnectionState = stateDto.ToString();
    }

    private async Task StartLumenDataStream()
    {
        if (IsLumenDataSelected)
        {
            await _featherDeviceService.StartLumenDataReceivingAsync(FeatherDevice);
        }
        else
        {
            LumenValue = NotSelected;
        }
    }

    private async Task StartDistanceDataStream()
    {
        if (IsDistanceDataSelected)
        {
            await _featherDeviceService.StartDistanceDataReceivingAsync(FeatherDevice);
        }
        else
        {
            DistanceValue = NotSelected;
        }
    }

    private async Task StartTemperatureDataStream()
    {
        if (IsTempDataSelected)
        {
            await _featherDeviceService.StartTemperatureDataReceivingAsync(FeatherDevice);
        }
        else
        {
            TemperatureValue = NotSelected;
        }
    }
}
