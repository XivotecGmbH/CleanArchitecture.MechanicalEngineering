using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Controls;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Controls;

public partial class BannerView
{
    public BannerView() : this(App.Current.Handler.MauiContext.Services.GetServices<BannerViewViewModel>().First())
    {
    }

    public BannerView(BannerViewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}