using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Controls;

public partial class MainMenuControl
{
    public MainMenuControl() : this(App.Current.Handler.MauiContext.Services.GetServices<MainMenuViewModel>().First())
    {
    }

    public MainMenuControl(MainMenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}