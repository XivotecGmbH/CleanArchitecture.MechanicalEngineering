import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {ToDoItemClient, ToDoItemDto, ToDoListClient, ToDoListDto} from "../../../services/apiClient/api.service";

@Component({
  selector: 'app-todoitem',
  templateUrl: './todoitem.component.html',
  styleUrl: './todoitem.component.css'
})
export class TodoitemComponent implements OnInit {
  public selectedList: ToDoListDto = {} as ToDoListDto;
  public filteredToDoItems: ToDoItemDto[] = [];
  public searchInput: string = "";
  public documentDarkThemePresent: boolean = document.body.classList.contains('dark-theme');

  public constructor(private router: Router, private toDoListClient: ToDoListClient, private toDoItemClient: ToDoItemClient) {
    this.selectedList = this.router.getCurrentNavigation()?.extras.state?.['currentlist'];

  }

  public ngOnInit(): void {
    this.getToDoList();
  }

  public addButtonClicked(): void {
    this.router.navigate(['/tododetail'], {
      state: {
        currentlist: this.selectedList
      }
    });
  }

  public editButtonClicked(selecteditem: ToDoItemDto): void {
    this.router.navigate(['/tododetail'], {
      state:
        {
          currentitem: selecteditem,
          currentlist: this.selectedList
        }
    });
  }

  public deleteButtonClicked(selectedItemId: string): void {
    this.deleteToDoItem(selectedItemId);
  }

  public markItemDone(selectedList: ToDoItemDto): void {
    this.changeItemDone(selectedList);
  }

  public searchFilterItems(): void {
    this.filteredToDoItems = this.selectedList.toDoItems
      .filter(item => item.title.toLowerCase().includes(this.searchInput.toLowerCase()));
  }

  private getToDoList(): void {
    this.toDoListClient.getById(this.selectedList.id).subscribe({
      next: result => {
        this.selectedList = result;
        this.searchFilterItems();
      },
      error: error => console.error(error),
    });
  }

  private deleteToDoItem(selectedItemId: string): void {
    this.toDoItemClient.deleteById(selectedItemId)
      .subscribe({
        next: () => this.getToDoList(),
        error: error => console.error(error)
      });
  }

  private changeItemDone(selectedItem: ToDoItemDto): void {
    selectedItem.done = !selectedItem.done;
    this.toDoItemClient.update(selectedItem)
      .subscribe({
        error: error => console.error(error)
      });
  }
}
