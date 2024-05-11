using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Notification;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Notification;

public partial class NotificationsPage
{
    public NotificationsPage(NotificationsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}