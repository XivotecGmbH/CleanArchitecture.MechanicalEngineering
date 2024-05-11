using System.Globalization;
using Xivotec.CleanArchitecture.Application.Notifications.Dtos;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Converter;

public class NotificationToNotificationMessageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        var dto = (NotificationDto)value;

        if (dto.Type == NotificationTypeDto.Error)
        {
            return $"Error: {dto.Title}";
        }

        return dto.Title;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //Only required for two-way binding.
        throw new NotImplementedException();
    }
}