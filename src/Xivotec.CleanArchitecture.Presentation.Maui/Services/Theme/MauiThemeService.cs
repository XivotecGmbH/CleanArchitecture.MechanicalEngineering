using Xivotec.CleanArchitecture.Presentation.Core.Services.Theme;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Services.Theme;

public class MauiThemeService : IThemeService
{
    public void ToggleTheme(bool value)
    {
        Microsoft.Maui.Controls.Application.Current!.UserAppTheme =
            value ? AppTheme.Light : AppTheme.Dark;
    }
}