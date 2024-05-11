using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Text.RegularExpressions;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec.CleanArchitecture.Presentation.Core.Messages;
using Xivotec.CleanArchitecture.Presentation.Core.Services.Navigation;

namespace Xivotec.CleanArchitecture.Presentation.Core.ViewModels.ToDoList;

public sealed partial class ToDoDetailViewModel : ViewModelBase
{
    [ObservableProperty]
    private ToDoItemDto _selectedItem = new();

    // Properties for ToDoItem UI fields
    [ObservableProperty]
    private string _note = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isNewItem;

    [ObservableProperty]
    private Color _itemEntryBorderColor;

    [ObservableProperty]
    private bool _isEntryErrorLabelActive;

    [ObservableProperty]
    private string _entryErrorLabelText = string.Empty;

    [ObservableProperty]
    private bool _isSaveUpdateButtonActive;

    private List<ToDoItemDto> _toDoItemsCollection = [];

    [GeneratedRegex("^(?! )[a-zA-Z0-9 ]+(?<! )$")]
    private partial Regex ValidEntryRegex();

    private readonly IMediator _mediator;

    public ToDoDetailViewModel(
        INavigationService navigation,
        ILogger<ToDoDetailViewModel> logger,
        IMediator mediator)
        : base(navigation, logger)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    public async Task SaveItem()
    {
        SelectedItem.Id = Guid.NewGuid();
        SelectedItem.Title = Title;
        SelectedItem.Note = Note;

        await _mediator.Send(new AddToDoItemCommand(SelectedItem));
        await NavigateToToDoItemWithMessage();
    }

    [RelayCommand]
    public async Task UpdateItem()
    {
        SelectedItem.Title = Title;
        SelectedItem.Note = Note;

        await _mediator.Send(new UpdateToDoItemCommand(SelectedItem));
        await NavigateToToDoItemWithMessage();
    }

    [RelayCommand]
    public async Task DeleteItem()
    {
        await _mediator.Send(new DeleteToDoItemCommand(SelectedItem));
        await NavigateToToDoItemWithMessage();
    }

    [RelayCommand]
    public async Task CancelItem()
        => await NavigateBackAsync();

    partial void OnTitleChanged(string value)
    {
        var isEntryEmpty = string.IsNullOrEmpty(value);
        var isEntryValid = ValidateEntry(value);
        var isEntryDuplicate = IsDuplicateEntry(value);

        IsSaveUpdateButtonActive = !isEntryEmpty && isEntryValid && !isEntryDuplicate;
        IsEntryErrorLabelActive = !isEntryEmpty && (!isEntryValid || isEntryDuplicate);

        if (IsEntryErrorLabelActive && !isEntryValid)
        {
            EntryErrorLabelText = "only letters, numbers and spaces allowed";
        }
        else if (IsEntryErrorLabelActive && isEntryDuplicate)
        {
            EntryErrorLabelText = "an item with this title already exists";
        }
        else
        {
            EntryErrorLabelText = string.Empty;
        }

        ItemEntryBorderColor = IsEntryErrorLabelActive ? Color.Red : Color.Gray;
    }

    protected override async Task ApplyNavigationValues(NavigationMessageDto dto)
    {
        SelectedItem = (ToDoItemDto)dto.Value;
        await UpdateSelectedToDoItemsCollectionAsync();

        Title = SelectedItem.Title;
        Note = SelectedItem.Note;

        IsNewItem = false;

        if (SelectedItem.Id.Equals(Guid.Empty))
        {
            IsNewItem = true;
        }
    }

    private bool ValidateEntry(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        return ValidEntryRegex().IsMatch(str);
    }

    private bool IsDuplicateEntry(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }

        return _toDoItemsCollection.FirstOrDefault(item => str.Equals(item.Title)
                && item.Id != SelectedItem.Id) is not null;
    }

    private async Task UpdateSelectedToDoItemsCollectionAsync()
    {
        var foundItemList = await _mediator.Send(new GetToDoListByIdQuery(SelectedItem.ListId));
        _toDoItemsCollection = foundItemList.ToDoItems;
    }

    private async Task NavigateToToDoItemWithMessage()
    {
        var selectedList = await _mediator.Send(new GetToDoListByIdQuery(SelectedItem.ListId));
        await Navigation.NavigateToAsync(nameof(ToDoItemViewModel), selectedList);
    }
}
