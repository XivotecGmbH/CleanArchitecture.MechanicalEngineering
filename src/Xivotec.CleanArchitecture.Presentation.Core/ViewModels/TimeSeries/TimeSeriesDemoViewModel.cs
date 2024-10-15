using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.TimeSeries;

public sealed partial class TimeSeriesDemoViewModel : ViewModelBase
{
    private const int EntriesPerInsert = 5;

    private const double MaximumTemperature = 30.0d;
    private readonly IMediator _mediator;
    private readonly TimeSpan _historyTimeSpan = TimeSpan.FromMinutes(5);

    [ObservableProperty]
    private List<TemperatureEntryDto> _temperatureEntries = new();

    [ObservableProperty]
    private bool _isLoadingScreenVisible;

    public TimeSeriesDemoViewModel(INavigationService navigation,
        ILogger<TimeSeriesDemoViewModel> logger,
        IMediator mediator) : base(navigation, logger)
    {
        _mediator = mediator;
    }

    public override async Task OnNavigatedTo()
    {
        await RefreshTemperatureEntries();
        await base.OnNavigatedTo();
    }

    [RelayCommand]
    public async Task SaveNewEntries()
    {
        var entries = GenerateEntriesPackage();
        await _mediator.Send(new AddTemperatureEntriesCommand(entries));
        await RefreshTemperatureEntries();
    }

    [RelayCommand]
    public async Task DeleteAllEntries()
    {
        await _mediator.Send(new DeleteTemperatureEntryRangeCommand(DateTime.UtcNow.Subtract(_historyTimeSpan), DateTime.UtcNow));
        await RefreshTemperatureEntries();
    }

    private List<TemperatureEntryDto> GenerateEntriesPackage()
    {
        var timestamp = DateTime.UtcNow;
        var entries = new List<TemperatureEntryDto>();

        for (int i = 0; i < EntriesPerInsert; i++)
        {
            entries.Add(new TemperatureEntryDto()
            {
                Timestamp = timestamp,
                Temperature = Random.Shared.NextDouble() * MaximumTemperature,
                Source = $"sensor {i}"
            });
        }

        return entries;
    }

    private async Task RefreshTemperatureEntries()
    {
        IsLoadingScreenVisible = true;
        var entries = await _mediator.Send(new GetTemperatureEntriesRangeQuery(
            DateTime.UtcNow.Subtract(_historyTimeSpan),
            DateTime.UtcNow));
        TemperatureEntries = entries;
        IsLoadingScreenVisible = false;
    }
}