import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToDoList } from '../../types';
import { Observable } from 'rxjs';
import { ToDoLoaderService } from 'src/app/todo-loader.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  @Input() toDoListData: ToDoList[];
  @Output() toDoListSelected = new EventEmitter<ToDoList>();
  currentToDoList: Observable<ToDoList>;

  constructor(svc: ToDoLoaderService) {
    this.currentToDoList = svc.currentToDoList;
  }
  ngOnInit() {
  }
  setCurrentToDoList(toDoList: ToDoList) {
    this.toDoListSelected.emit(toDoList);
  }
}
