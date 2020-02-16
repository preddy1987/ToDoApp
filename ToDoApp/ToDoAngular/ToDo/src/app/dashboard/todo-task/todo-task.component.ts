import { Component, OnInit, Input } from '@angular/core';
import { ToDoList } from 'src/app/types';

@Component({
  selector: 'app-todo-task',
  templateUrl: './todo-task.component.html',
  styleUrls: ['./todo-task.component.css']
})
export class ToDoTaskComponent implements OnInit {
  @Input() toDoList: ToDoList;
  constructor() { }

  ngOnInit() {
  }

}
