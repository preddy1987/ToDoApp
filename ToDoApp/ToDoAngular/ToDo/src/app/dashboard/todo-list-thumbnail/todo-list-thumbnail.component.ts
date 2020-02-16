import { Component, OnInit, Input } from '@angular/core';
import { ToDoList } from '../../types';

@Component({
  selector: 'app-todo-list-thumbnail',
  templateUrl: './todo-list-thumbnail.component.html',
  styleUrls: ['./todo-list-thumbnail.component.css']
})
export class TodoListThumbnailComponent implements OnInit {
@Input() toDoList: ToDoList;
  constructor() { }

  ngOnInit() {
  }

}
