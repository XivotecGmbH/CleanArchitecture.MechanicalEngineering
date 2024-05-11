using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Recipe.FeatherDevice;

public partial class FeatherDeviceRecipeImportPage
{
    public FeatherDeviceRecipeImportPage(FeatherDeviceRecipeImportViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}