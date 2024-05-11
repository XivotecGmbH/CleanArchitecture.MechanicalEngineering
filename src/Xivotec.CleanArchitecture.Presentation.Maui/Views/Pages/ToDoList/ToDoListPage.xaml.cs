using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.ToDoList;

public partial class ToDoListPage
{
    public ToDoListPage(ToDoListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}