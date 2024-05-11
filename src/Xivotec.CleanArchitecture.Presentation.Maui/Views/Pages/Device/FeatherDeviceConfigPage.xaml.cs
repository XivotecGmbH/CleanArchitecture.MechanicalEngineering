using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Device;

public partial class FeatherDeviceConfigPage
{
    public FeatherDeviceConfigPage(FeatherDeviceConfigViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}