using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Device;

public partial class FeatherDeviceControlPage
{
    public FeatherDeviceControlPage(FeatherDeviceControlViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}