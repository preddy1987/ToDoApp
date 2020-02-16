import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

import { Task, ToDoList } from '../app/types';

const apiUrl = '/assets/dummydata/toDoListData.json';

@Injectable({
  providedIn: 'root'
})
export class ToDoLoaderService {
  constructor(private http: HttpClient) {}

  _currentToDoList: ToDoList;
  currentToDoList = new BehaviorSubject(undefined);


  setCurrentToDoList(toDoList: ToDoList) {
    this.currentToDoList.next(toDoList);
  }


  loadToDoLists(): Observable<ToDoList[]> {
    return this.http
      .get<ToDoList[]>(apiUrl)
  }
}
