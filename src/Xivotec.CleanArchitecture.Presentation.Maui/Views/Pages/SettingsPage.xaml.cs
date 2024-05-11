using Xivotec.CleanArchitecture.Presentation.Core.ViewModels;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages;

public partial class SettingsPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}