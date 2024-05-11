using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Device;

public partial class DevicePage
{
    public DevicePage(DeviceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}