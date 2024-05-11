using Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Notification;

namespace Xivotec.CleanArchitecture.Presentation.Maui.Views.Pages.Notification;

public partial class NotificationDetailsPage
{
    public NotificationDetailsPage(NotificationDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}