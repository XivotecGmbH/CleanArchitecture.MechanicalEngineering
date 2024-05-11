using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.ToDoList;

public partial class ToDoDetailPage
{
    public ToDoDetailPage(ToDoDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}