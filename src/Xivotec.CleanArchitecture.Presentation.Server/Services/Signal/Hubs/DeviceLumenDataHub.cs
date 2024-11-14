using Microsoft.AspNetCore.SignalR;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Messages;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Hubs;

public class DeviceLumenDataHub : Hub, ISignalHub<LumenDataChangedMessage>
{
    private const string LumenSignalMethod = "LumenDataMessage";
    private readonly IHubContext<DeviceLumenDataHub> _hubContext;

    public DeviceLumenDataHub(IHubContext<DeviceLumenDataHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(LumenDataChangedMessage message)
    {
        await _hubContext.Clients.All.SendAsync(LumenSignalMethod, message.Value);
    }
}