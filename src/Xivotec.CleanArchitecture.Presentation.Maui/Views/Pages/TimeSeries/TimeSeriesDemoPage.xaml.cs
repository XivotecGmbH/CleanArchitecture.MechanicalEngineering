using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.TimeSeries;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.TimeSeries;

public partial class TimeSeriesDemoPage
{
    public TimeSeriesDemoPage(TimeSeriesDemoViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}