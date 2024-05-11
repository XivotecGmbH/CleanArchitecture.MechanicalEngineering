using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe.FeatherDevice;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Recipe.FeatherDevice;

public partial class FeatherDeviceRecipeDetailPage
{
	public FeatherDeviceRecipeDetailPage(FeatherDeviceRecipeDetailViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}