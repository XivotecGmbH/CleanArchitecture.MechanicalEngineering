using Microsoft.AspNetCore.SignalR;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Services.Signal.Hubs;

public class DeviceTemperatureDataHub : Hub, ISignalHub<TemperatureDataChangedMessage>
{
    private const string TemperatureSignalMethod = "TemperatureDataMessage";
    private readonly IHubContext<DeviceTemperatureDataHub> _hubContext;

    public DeviceTemperatureDataHub(IHubContext<DeviceTemperatureDataHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(TemperatureDataChangedMessage message)
    {
        await _hubContext.Clients.All.SendAsync(TemperatureSignalMethod, message.Value);
    }
}