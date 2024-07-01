using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Drawing;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

public partial class FeatherDeviceControlViewModel : ViewModelBase,
    IRecipient<ConnectionStateChangedMessage>,
    IRecipient<DistanceDataChangedMessage>,
    IRecipient<TemperatureDataChangedMessage>,
    IRecipient<LumenDataChangedMessage>
{
    private readonly IMediator _mediator;
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

    public FeatherDeviceControlViewModel(
        INavigationService navigation,
        ILogger<FeatherDeviceControlViewModel> logger,
        IMediator mediator)
        : base(navigation, logger)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task ConnectAsync() => await _mediator.Send(new ConnectFeatherDeviceCommand(FeatherDevice));

    [RelayCommand]
    public async Task DisconnectAsync() => await _mediator.Send(new DisconnectFeatherDeviceCommand(FeatherDevice));

    [RelayCommand]
    public async Task StartAsync()
    {
        await _mediator.Send(new StartFeatherDeviceCommand(FeatherDevice));
        await StartTemperatureDataStream();
        await StartDistanceDataStream();
        await StartLumenDataStream();
    }

    [RelayCommand]
    public async Task StopAsync() => await _mediator.Send(new StopFeatherDeviceCommand(FeatherDevice));

    [RelayCommand]
    public async Task PauseAsync() => await _mediator.Send(new PauseFeatherDeviceCommand(FeatherDevice));

    [RelayCommand]
    public async Task ContinueAsync() => await _mediator.Send(new ContinueFeatherDeviceCommand(FeatherDevice));


    public void Receive(ConnectionStateChangedMessage message) => UpdateConnectionState(message.Value ? ConnectionStateDto.Connected : ConnectionStateDto.Disconnected);

    public void Receive(TemperatureDataChangedMessage message) => TemperatureValue = message.Value;

    public void Receive(DistanceDataChangedMessage message) => DistanceValue = message.Value;

    public void Receive(LumenDataChangedMessage message) => LumenValue = message.Value;

    protected override async Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        FeatherDevice = (FeatherDeviceDto)dto.Value;

        var state = await _mediator.Send(new GetConnectionStateFeatherDeviceQuery(FeatherDevice));
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
            await _mediator.Send(new StartLumenDataReceivingFeatherDeviceCommand(FeatherDevice));
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
            await _mediator.Send(new StartDistanceDataReceivingFeatherDeviceCommand(FeatherDevice));
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
            await _mediator.Send(new StartTemperatureDataReceivingFeatherDeviceCommand(FeatherDevice));
        }
        else
        {
            TemperatureValue = NotSelected;
        }
    }
}
