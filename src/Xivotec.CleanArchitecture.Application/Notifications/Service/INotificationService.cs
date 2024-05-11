using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Domain.NotificationAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.Notifications.Service;

/// <summary>
/// Service for managing notifications within the application.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Creates a new <see cref="Notification"/>, saves it to a RuntimeRepository and sends a <see cref="NotificationMessage"/> with the created <see cref="NotificationDto"/>.
    /// </summary>
    /// <param name="notification">The notification to be saved.</param>
    /// <returns><see cref="Task.CompletedTask"/> on completion.</returns>
    Task CreateNotification(NotificationDto notification);

    /// <summary>
    /// Returns all <see cref="NotificationDto"/> from the RuntimeRepository.
    /// </summary>
    /// <returns>All <see cref="NotificationDto"/> from the RuntimeRepository.</returns>
    Task<List<NotificationDto>> GetAllNotifications();

    /// <summary>
    /// Sets the passed <see cref="NotificationDto"/> as acknowledged in the RuntimeRepository.
    /// </summary>
    /// <param name="notification">The notification to be saved.</param>
    /// <returns><see cref="Task.CompletedTask"/> on completion.</returns>
    Task AcknowledgeNotification(NotificationDto notification);
}