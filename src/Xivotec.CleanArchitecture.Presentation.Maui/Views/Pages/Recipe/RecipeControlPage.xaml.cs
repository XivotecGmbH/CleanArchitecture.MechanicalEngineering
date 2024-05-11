using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Recipe;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Recipe;

public partial class RecipeControlPage
{
	public RecipeControlPage(RecipeControlViewModel recipeControlViewModel)
	{
		InitializeComponent();
        BindingContext = recipeControlViewModel;
    }
}