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
  public ToDoListCollection: ToDoListDto[] = [];
  public TitleInput: string = "";
  public Sorting: number = 0;
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
        this.ToDoListCollection = result;
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
    if (this.TitleInput == "") {
      return;
    }

    const data: ToDoListDto = {
      id: Guid.create().toString(),
      title: this.TitleInput,
      toDoItems: []
    }
    this.toDoListClient.create(data)
      .subscribe({
        next: () => this.getToDoLists(),
        error: error => console.error(error)
      });

    this.TitleInput = "";
  }
}
