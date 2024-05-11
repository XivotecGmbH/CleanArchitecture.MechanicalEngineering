using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Controls;

public partial class FeatherDeviceItemView
{
    public FeatherDeviceItemView() : this(App.Current.Handler.MauiContext.Services.GetServices<FeatherDeviceItemViewModel>().First())
    {
    }

    public FeatherDeviceItemView(FeatherDeviceItemViewModel featherDeviceItemViewModel)
    {
        InitializeComponent();
        BindingContext = featherDeviceItemViewModel;
    }
}