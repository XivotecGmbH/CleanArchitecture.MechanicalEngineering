import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {ToDoItemClient, ToDoItemDto, ToDoListClient, ToDoListDto} from "../../../services/apiClient/api.service";

@Component({
  selector: 'app-todoitem',
  templateUrl: './todoitem.component.html',
  styleUrl: './todoitem.component.css'
})
export class TodoitemComponent {
  public SelectedList: ToDoListDto = {} as ToDoListDto;
  public SearchInput: string = "";
  public documentDarkThemePresent: boolean = document.body.classList.contains('dark-theme');

  public constructor(private router: Router, private toDoListClient: ToDoListClient, private toDoItemClient: ToDoItemClient) {
    this.SelectedList = this.router.getCurrentNavigation()?.extras.state?.['currentlist'];
    this.getToDoList();
  }

  public addButtonClicked(): void {
    this.router.navigate(['/tododetail'], {
      state: {
        currentlist: this.SelectedList
      }
    });
  }

  public editButtonClicked(selecteditem: ToDoItemDto): void {
    this.router.navigate(['/tododetail'], {
      state:
        {
          currentitem: selecteditem,
          currentlist: this.SelectedList
        }
    });
  }

  public deleteButtonClicked(selectedItemId: string): void {
    this.deleteToDoItem(selectedItemId);
  }

  public markItemDone(selectedList: ToDoItemDto): void {
    this.changeItemDone(selectedList);
  }

  private getToDoList(): void {
    this.toDoListClient.getById(this.SelectedList.id).subscribe({
      next: result => {
        this.SelectedList = result;
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
