using Microsoft.AspNetCore.SignalR;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Hubs;

public class DeviceConnectionStateHub : Hub, ISignalHub<ConnectionStateChangedMessage>
{
    private const string ConnectionStateSignalMethod = "ConnectionStateMessage";
    private readonly IHubContext<DeviceConnectionStateHub> _hubContext;

    public DeviceConnectionStateHub(IHubContext<DeviceConnectionStateHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(ConnectionStateChangedMessage message)
    {
        await _hubContext.Clients.All.SendAsync(ConnectionStateSignalMethod, message.Value);
    }
}