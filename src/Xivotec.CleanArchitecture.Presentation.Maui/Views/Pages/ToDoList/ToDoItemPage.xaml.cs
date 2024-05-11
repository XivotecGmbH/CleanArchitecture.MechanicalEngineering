using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.ToDoList;

public partial class ToDoItemPage
{
    public ToDoItemPage(ToDoItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}