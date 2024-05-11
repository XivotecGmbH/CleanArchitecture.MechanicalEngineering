using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

public sealed partial class ToDoItemViewModel :
    ViewModelBase
{
    [ObservableProperty]
    private ToDoListDto _selectedList = new();

    [ObservableProperty]
    private ObservableCollection<ToDoItemDto> _toDoItemsCollection = [];

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _searchText = string.Empty;

    private List<ToDoItemDto> _toDoItemsList = [];

    private readonly IMediator _mediator;

    public ToDoItemViewModel(
        INavigationService navigation,
        ILogger<ToDoItemViewModel> logger,
        IMediator mediator)
        : base(navigation, logger)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task AddNewItem()
    {
        var newItem = new ToDoItemDto
        {
            Id = Guid.Empty,
            ListId = SelectedList.Id
        };

        await EditItem(newItem);
    }

    [RelayCommand]
    public async Task EditItem(ToDoItemDto selectedItem)
    {
        await Navigation.NavigateToAsync(nameof(ToDoDetailViewModel), selectedItem);
    }

    [RelayCommand]
    public async Task DeleteItem(ToDoItemDto selectedItem)
    {
        await _mediator.Send(new DeleteToDoItemCommand(selectedItem));
        _toDoItemsList.Remove(selectedItem);
        OnSearchTextChanged(SearchText);
    }

    [RelayCommand]
    public async Task ItemDoneTapped(ToDoItemDto selectedItem)
    {
        selectedItem.Done = !selectedItem.Done;
        await _mediator.Send(new UpdateToDoItemCommand(selectedItem));
        SortItems();
    }

    partial void OnSearchTextChanged(string value)
    {
        // get the items that match
        var itemsMatchSearch = _toDoItemsList
            .Where(x => x.Title.Contains(value))
            .OrderBy(item => item.Title);

        ToDoItemsCollection = new ObservableCollection<ToDoItemDto>(itemsMatchSearch);
    }

    [RelayCommand]
    public async Task NavigateBack()
        => await Navigation.NavigateBackAsync();

    private async Task RefreshItems()
    {
        var foundItemList = await _mediator.Send(new GetToDoListByIdQuery(SelectedList.Id));
        _toDoItemsList = [.. foundItemList.ToDoItems];
        SortItems();
    }

    private void SortItems()
    {
        // reset ObservableCollection
        ToDoItemsCollection = new ObservableCollection<ToDoItemDto>(_toDoItemsList);

        // separate List in ticked and un-ticked, sorted by title
        var tickedItems = ToDoItemsCollection
            .Where(x => x.Done)
            .OrderBy(item => item.Title);

        var unTickedItems = ToDoItemsCollection
            .Where(x => !x.Done)
            .OrderBy(item => item.Title);

        // recombine resulting lists
        var combinedItems = unTickedItems.Concat(tickedItems);
        ToDoItemsCollection = new ObservableCollection<ToDoItemDto>(combinedItems);
    }

    protected override async Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        SelectedList = (ToDoListDto)dto.Value;

        await RefreshItems();
        Title = SelectedList.Title;
    }
}
