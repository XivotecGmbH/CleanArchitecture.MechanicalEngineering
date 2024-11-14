using Microsoft.AspNetCore.SignalR;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Hubs;

public class DeviceDistanceDataHub : Hub, ISignalHub<DistanceDataChangedMessage>
{
    private const string DistanceSignalMethod = "DistanceDataMessage";
    private readonly IHubContext<DeviceDistanceDataHub> _hubContext;

    public DeviceDistanceDataHub(IHubContext<DeviceDistanceDataHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(DistanceDataChangedMessage message)
    {
        await _hubContext.Clients.All.SendAsync(DistanceSignalMethod, message.Value);
    }
}