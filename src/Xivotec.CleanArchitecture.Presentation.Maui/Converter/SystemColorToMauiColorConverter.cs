using System.Globalization;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Converter;

public class SystemColorToMauiColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var systemColor = (System.Drawing.Color)value!;

        var red = systemColor.R / 255f;
        var green = systemColor.G / 255f;
        var blue = systemColor.B / 255f;
        var alpha = systemColor.A / 255f;

        return new Color(red, green, blue, alpha);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}