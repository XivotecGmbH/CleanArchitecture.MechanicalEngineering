import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Guid} from 'guid-typescript';
import {ToDoListClient, ToDoListDto} from "../../services/apiClient/api.service";

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrl: './todolist.component.css'
})

export class TodolistComponent implements OnInit {
  public toDoListCollection: ToDoListDto[] = [];
  public titleInput: string = "";
  public sorting: string = "0";
  public documentDarkThemePresent: boolean = document.body.classList.contains('dark-theme');

  public constructor(private router: Router, private toDoListClient: ToDoListClient) {
  }

  public ngOnInit(): void {
    this.getToDoLists();
  }

  public deleteButtonClicked(selectedToDoList: ToDoListDto): void {
    this.deleteToDoList(selectedToDoList);
  }

  public addButtonClicked(): void {
    this.addNewToDoList();
  }

  public NavigateButtonClicked(list: ToDoListDto): void {
    this.router.navigate(['/todoitem'], {state: {currentlist: list}});
  }

  private getToDoLists(): void {
    this.toDoListClient.getAll().subscribe({
      next: result => {
        this.toDoListCollection = result;
        this.sortLists();
      },
      error: error => console.error(error),
    });
  }

  private deleteToDoList(selectedToDoList: ToDoListDto): void {
    this.toDoListClient.deleteById(selectedToDoList.id)
      .subscribe({
        next: () => this.getToDoLists(),
        error: error => console.error(error),
      });
  }

  private addNewToDoList(): void {
    if (this.titleInput == "") {
      return;
    }

    const data: ToDoListDto = {
      id: Guid.create().toString(),
      title: this.titleInput,
      toDoItems: []
    }
    this.toDoListClient.create(data)
      .subscribe({
        next: () => this.getToDoLists(),
        error: error => console.error(error)
      });

    this.titleInput = "";
  }

  protected sortLists(): void {
    switch (parseInt(this.sorting, 10)) {
      case 1:
        this.toDoListCollection.reverse();
        break;
      case 2:
        this.toDoListCollection.sort((a, b) => a.title.localeCompare(b.title));
        break;
      case 3:
        this.toDoListCollection.sort((a, b) => b.title.localeCompare(a.title));
        break;
      case 4:
        this.toDoListCollection.sort((a, b) => a.title.localeCompare(b.title))
          .sort((a, b) => a.title.length - b.title.length);
        break;
      case 5:
        this.toDoListCollection.sort((a, b) => a.title.localeCompare(b.title))
          .sort((a, b) => b.title.length - a.title.length);
        break;
      default:
        break;
    }
  }
}
