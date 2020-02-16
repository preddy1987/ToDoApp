import { Component, OnInit } from '@angular/core';
import { ToDoList, Task } from '../../types';
import { ToDoLoaderService } from 'src/app/todo-loader.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-todo-dashboard',
  templateUrl: './todo-dashboard.component.html',
  styleUrls: ['./todo-dashboard.component.css']
})
export class TodoDashboardComponent implements OnInit {
  toDoLists: Observable<ToDoList[]>;
  selectedToDoList: ToDoList | undefined;
  
  constructor(private svc: ToDoLoaderService) {
    this.toDoLists = svc.loadToDoLists();
    console.log(this.toDoLists);
  }

  ngOnInit() {
  }
  setSelectedToDoList(toDoList: ToDoList) {
    this.svc.setCurrentToDoList(toDoList);
  }
}
