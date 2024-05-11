namespace Xivotec.CleanArchitecture.Presentation.Maui;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Window.MinimumHeight = 450;
        Window.MinimumWidth = 800;
    }
}