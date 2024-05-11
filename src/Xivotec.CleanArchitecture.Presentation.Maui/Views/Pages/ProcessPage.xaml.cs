using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Processes;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages;

public partial class ProcessPage
{
    public ProcessPage(ProcessViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}