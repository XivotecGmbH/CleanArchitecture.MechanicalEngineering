using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.Device;

public partial class DeviceViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<string> _serviceNames = [];

    [ObservableProperty]
    private string _selectedItem = string.Empty;

    public DeviceViewModel(
        INavigationService navigation,
        ILogger<DeviceViewModel> logger,
        IEnumerable<IDeviceServiceBase> services)
        : base(navigation, logger)
    {
        AddImplementedServices(services);
    }

    [RelayCommand]
    public async Task AddDevice()
    {
        if (string.IsNullOrEmpty(SelectedItem))
        {
            return;
        }

        var selectedDeviceName = SelectedItem + "ConfigViewModel";
        await Navigation.NavigateToAsync(selectedDeviceName);
    }

    public override async Task PageAppearing()
    {
        await base.PageAppearing();

        WeakReferenceMessenger.Default.Send(new DevicePageAppearingMessage(true));
    }

    private void AddImplementedServices(IEnumerable<IDeviceServiceBase> services)
    {
        foreach (var service in services)
        {
            var name = service.GetType().Name;
            name = name.Replace("Service", "");

            ServiceNames.Add(name);
        }

        if (ServiceNames.Count == 1)
        {
            SelectedItem = ServiceNames[0];
        }
    }
}
