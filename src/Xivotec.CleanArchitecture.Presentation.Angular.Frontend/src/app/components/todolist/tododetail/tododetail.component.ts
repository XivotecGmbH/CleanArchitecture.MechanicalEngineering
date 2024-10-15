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
  private readonly CurrentList: ToDoListDto = {} as ToDoListDto;
  public SelectedItem: ToDoItemDto = {} as ToDoItemDto;
  public IsSaveButtonEnabled: boolean = true;
  public IsUpdateButtonEnabled: boolean = true;
  public IsDeleteButtonEnabled: boolean = true;

  public constructor(private router: Router, private toDoItemClient: ToDoItemClient) {
    if (this.router.getCurrentNavigation()?.extras.state?.['currentitem'] != null) {
      this.SelectedItem = this.router.getCurrentNavigation()?.extras.state?.['currentitem'];
    }
    this.CurrentList = this.router.getCurrentNavigation()?.extras.state?.['currentlist'];
  }

  public ngOnInit(): void {
    this.changeButtonsEnabledState();
  }

  public applyInputs(operation: string): void {
    switch (operation) {
      case "save":
        this.saveToDoItem(this.SelectedItem);
        break;
      case "update":
        this.updateToDoItem(this.SelectedItem);
        break;
      case "delete":
        this.deleteToDoItem(this.SelectedItem);
        break;
      default:
        console.error("Unknown input operation received: " + operation);
        break;
    }
  }

  private deleteToDoItem(itemToDelete: ToDoItemDto): void {
    this.toDoItemClient.deleteById(itemToDelete.id)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.CurrentList}}),
        error: error => console.error(error)
      });
  }

  private updateToDoItem(itemToUpdate: ToDoItemDto): void {
    this.toDoItemClient.update(itemToUpdate)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.CurrentList}}),
        error: error => console.error(error)
      });
  }

  private saveToDoItem(item: ToDoItemDto): void {
    const itemToSave: ToDoItemDto = {
      id: Guid.create().toString(),
      listId: this.CurrentList.id,
      title: item.title,
      note: item.note,
      reminder: item.reminder,
      done: item.done
    }

    this.toDoItemClient.create(itemToSave)
      .subscribe({
        next: () => this.router.navigate(['/todoitem'], {state: {currentlist: this.CurrentList}}),
        error: error => console.error(error)
      });
  }

  private changeButtonsEnabledState(): void {
    if (Object.keys(this.SelectedItem).length == 0) {
      this.IsSaveButtonEnabled = true;
      this.IsUpdateButtonEnabled = false;
      this.IsDeleteButtonEnabled = false;
    } else {
      this.IsSaveButtonEnabled = false;
      this.IsUpdateButtonEnabled = true;
      this.IsDeleteButtonEnabled = true;
    }
  }
}
