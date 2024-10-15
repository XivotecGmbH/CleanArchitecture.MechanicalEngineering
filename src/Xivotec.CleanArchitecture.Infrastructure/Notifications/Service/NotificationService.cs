using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.Messages;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Application.Notifications.Service;
using Xivotec.CleanArchitecture.Domain.NotificationAggregate.Entities;

namespace Xivotec.CleanArchitecture.Infrastructure.Notifications.Service;

/// <inheritdoc cref="INotificationService"/>
public class NotificationService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly IRelationalRepository<Notification> _repository;

    public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _repository = unitOfWork.GetRelationalRepository<Notification>();
    }

    public async Task CreateNotification(NotificationDto notification)
    {
        var notificationEntity = _mapper.Map<Notification>(notification);
        await _repository.AddAsync(notificationEntity);

        WeakReferenceMessenger.Default.Send(new NotificationMessage(notification));
    }

    public async Task<List<NotificationDto>> GetAllNotifications()
    {
        var notifications = await _repository.GetAllAsync();
        var notificationDtos = notifications.Select(_mapper.Map<NotificationDto>).ToList();
        return notificationDtos;
    }

    public async Task AcknowledgeNotification(NotificationDto notification)
    {
        var notificationToAck = await _repository.GetByIdAsync(notification.Id);
        notificationToAck.Acknowledged = true;

        await _repository.UpdateAsync(notificationToAck);
    }
}