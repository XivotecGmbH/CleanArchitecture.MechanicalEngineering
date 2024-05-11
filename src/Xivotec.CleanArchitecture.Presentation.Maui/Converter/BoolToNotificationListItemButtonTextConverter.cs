using System.Globalization;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Converter;

public class BoolToNotificationListItemButtonTextConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value! ? "\u2714" : "Confirm";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //Only required for two-way binding.
        throw new NotImplementedException();
    }
}