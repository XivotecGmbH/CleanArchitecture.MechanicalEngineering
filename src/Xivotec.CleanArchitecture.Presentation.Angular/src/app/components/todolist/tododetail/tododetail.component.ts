import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Guid} from 'guid-typescript';
import {ToDoItemClient, ToDoItemDto, ToDoListDto} from "../../../services/apiClient/api.service";

@Component({
  selector: 'app-tododetail',
  templateUrl: './tododetail.component.html',
  styleUrl: './tododetail.component.css'
})
export class TododetailComponent implements OnInit {
  private readonly currentList: ToDoListDto = {} as ToDoListDto;
  public selectedItem: ToDoItemDto = {} as ToDoItemDto;
  public isSaveButtonEnabled: boolean = true;
  public isUpdateButtonEnabled: boolean = true;
  public isDeleteButtonEnabled: boolean = true;

  public constructor(private router: Router, private toDoItemClient: ToDoItemClient) {
    if (this.router.getCurrentNavigation()?.extras.state?.['currentitem'] != null) {
      this.selectedItem = this.router.getCurrentNavigation()?.extras.state?.['currentitem'];
    }
    this.currentList = this.router.getCurrentNavigation()?.extras.state?.['currentlist'];
  }

  public ngOnInit(): void {
    this.changeButtonsEnabledState();
  }

  public applyInputs(operation: string): void {
    switch (operation) {
      case "save":
        this.saveToDoItem(this.selectedItem);
        break;
      case "update":
        this.updateToDoItem(this.selectedItem);
        break;
      case "delete":
        this.deleteToDoItem(this.selectedItem);
        break;
      default:
        console.error("Unknown input operation received: " + operation);
        break;
    }
  }

  private deleteToDoItem(itemToDelete: ToDoItemDto): void {
    this.toDoItemClient.deleteById(itemToDelete.id)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.currentList}}),
        error: error => console.error(error)
      });
  }

  private updateToDoItem(itemToUpdate: ToDoItemDto): void {
    this.toDoItemClient.update(itemToUpdate)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.currentList}}),
        error: error => console.error(error)
      });
  }

  private saveToDoItem(item: ToDoItemDto): void {
    const itemToSave: ToDoItemDto = {
      id: Guid.create().toString(),
      listId: this.currentList.id,
      title: item.title,
      note: item.note,
      reminder: item.reminder,
      done: item.done
    }

    this.toDoItemClient.create(itemToSave)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.currentList}}),
        error: error => console.error(error)
      });
  }

  private changeButtonsEnabledState(): void {
    if (Object.keys(this.selectedItem).length == 0) {
      this.isSaveButtonEnabled = true;
      this.isUpdateButtonEnabled = false;
      this.isDeleteButtonEnabled = false;
    } else {
      this.isSaveButtonEnabled = false;
      this.isUpdateButtonEnabled = true;
      this.isDeleteButtonEnabled = true;
    }
  }
}
