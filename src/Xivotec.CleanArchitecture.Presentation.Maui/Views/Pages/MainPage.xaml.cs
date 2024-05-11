using Xivotec.CleanArchitecture.Presentation.Core.ViewModels;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages;

public partial class MainPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}