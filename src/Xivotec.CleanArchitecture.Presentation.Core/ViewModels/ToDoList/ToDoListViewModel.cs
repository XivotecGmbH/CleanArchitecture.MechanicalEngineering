using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text.RegularExpressions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

public sealed partial class ToDoListViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ToDoListDto> _toDoListsCollection;

    [ObservableProperty]
    private ToDoListDto _selectedList = new();

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isAddButtonActive;

    [ObservableProperty]
    private bool _isErrorLabelActive;

    [ObservableProperty]
    private Color _borderColor = Color.LightGray;

    [ObservableProperty]
    private int _sortOrderIndex;

    private readonly IMediator _mediator;

    public ToDoListViewModel(
        INavigationService navigation,
        ILogger<ToDoListViewModel> logger,
        IMediator mediator)
        : base(navigation, logger)
    {
        _mediator = mediator;

        IsAddButtonActive = false;
        ToDoListsCollection = [];
    }

    [RelayCommand]
    public async Task AddNewList()
    {
        // redundant as button is only visible if title contains at least one Char.
        if (Title == string.Empty)
        {
            return;
        }

        var newToDoList = new ToDoListDto
        {
            Id = Guid.NewGuid(),
            Title = Title
        };
        await _mediator.Send(new AddToDoListCommand(newToDoList));

        // reset & update UI
        ToDoListsCollection.Add(newToDoList);
        SortLists(SortOrderIndex);
        Title = string.Empty;
    }

    [RelayCommand]
    public async Task ListTapped(ToDoListDto selectedList)
    {
        await Navigation.NavigateToAsync(nameof(ToDoItemViewModel), selectedList);
    }

    [RelayCommand]
    public async Task DeleteList(ToDoListDto selectedToDoList)
    {
        await _mediator.Send(new DeleteToDoListCommand(selectedToDoList));
        ToDoListsCollection.Remove(selectedToDoList);
    }

    partial void OnTitleChanged(string value)
    {
        var isEntryValid = ValidateEntry(value);
        var isEntryEmpty = value == string.Empty;

        IsAddButtonActive = isEntryValid;
        IsErrorLabelActive = !isEntryValid && !isEntryEmpty;
        BorderColor = IsErrorLabelActive ? Color.Red : Color.LightGray;
    }

    partial void OnSortOrderIndexChanged(int value)
        => SortLists(value);

    public override async Task PageAppearing()
    {
        ToDoListsCollection = await RefreshList();
        SortLists(SortOrderIndex);
    }

    private async Task<ObservableCollection<ToDoListDto>> RefreshList()
    {
        var queryResult = await _mediator.Send(new GetToDoListAllQuery());
        return new ObservableCollection<ToDoListDto>(queryResult);
    }

    [GeneratedRegex("^[a-zA-Z]+$")]
    private partial Regex EntryValidRegex();

    private bool ValidateEntry(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        return EntryValidRegex().IsMatch(str);
    }

    private async void SortLists(int index)
    {
        ToDoListsCollection = index switch
        {
            1 => new(ToDoListsCollection.Reverse()),
            2 => new(ToDoListsCollection.OrderBy(item => item.Title)),
            3 => new(ToDoListsCollection.OrderByDescending(item => item.Title)),
            4 => new(ToDoListsCollection.OrderBy(item => item.Title)
                    .ThenBy(item => item.Title.Length)),
            5 => new(ToDoListsCollection.OrderBy(item => item.Title)
                    .ThenByDescending(item => item.Title.Length)),
            _ => ToDoListsCollection
        };
        await Task.CompletedTask;
    }
}
