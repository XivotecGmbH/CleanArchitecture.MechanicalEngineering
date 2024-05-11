using AutoMapper;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;
using Xivotec.CleanArchitecture.Application.RecipeFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;
using Xivotec.CleanArchitecture.Domain.NotificationAggregate.Entities;
using Xivotec.CleanArchitecture.Domain.NotificationAggregate.Enums;
using Xivotec.CleanArchitecture.Domain.RecipeAggregate;
using Xivotec.CleanArchitecture.Domain.ToDoListAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.Common.Converter;

public class MappingProfiles : Profile
{
    /// <summary>
    /// Defines all available AutoMapper Profiles within the Application
    /// </summary>
    public MappingProfiles()
    {
        _ = CreateMap<ToDoList, ToDoListDto>().ReverseMap();
        _ = CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();

        _ = CreateMap<FeatherDevice, FeatherDeviceDto>().ReverseMap();
        _ = CreateMap<XivotecRecipe, XivotecRecipeDto>().ReverseMap();
        _ = CreateMap<FeatherDeviceRecipe, FeatherDeviceRecipeDto>().ReverseMap();
        _ = CreateMap<LedColor, LedColorDto>().ReverseMap();

        _ = CreateMap<NotificationType, NotificationTypeDto>().ReverseMap();
        _ = CreateMap<Notification, NotificationDto>().ReverseMap();
    }
}
