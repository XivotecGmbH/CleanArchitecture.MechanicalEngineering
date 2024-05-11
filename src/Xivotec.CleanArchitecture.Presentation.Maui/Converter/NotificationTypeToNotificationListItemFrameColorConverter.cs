using System.Globalization;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Converter;

public class NotificationTypeToNotificationListItemFrameColorConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        var dto = (NotificationDto)value;

        if (!dto.Acknowledged && dto.Type == NotificationTypeDto.Error)
        {
            return Color.FromArgb("FF3333");
        }
        return Color.FromArgb("EBEBEB");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //Only required for two-way binding. This converter is only used for dto -> color.
        throw new NotImplementedException();
    }
}