using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Application.Notifications.Service;

namespace Xivotec.CleanArchitecture.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationDto>>> GetAll()
    {
        var notifications = await _notificationService.GetAllNotifications();
        return Ok(notifications);
    }

    [HttpPut]
    public async Task<ActionResult<NotificationDto>> Update([FromBody] NotificationDto notificationDto)
    {
        await _notificationService.AcknowledgeNotification(notificationDto);
        return Ok(notificationDto);
    }
}