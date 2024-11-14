using Microsoft.AspNetCore.SignalR;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Interfaces;

namespace Xivotec.CleanArchitecture.Presentation.Server.Services.Signal.Hubs;

public class NotificationHub : Hub, ISignalHub<NotificationMessage>
{
    private const string NotificationSignalMethod = "NotificationMessage";
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHub(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(NotificationMessage message)
    {
        await _hubContext.Clients.All.SendAsync(NotificationSignalMethod, message.Value);
    }
}