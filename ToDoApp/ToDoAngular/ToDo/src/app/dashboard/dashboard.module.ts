import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { TodoDashboardComponent } from './todo-dashboard/todo-dashboard.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { ToDoTaskComponent } from './todo-task/todo-task.component';
import { TodoListThumbnailComponent } from './todo-list-thumbnail/todo-list-thumbnail.component';



const dashboardRoutes: Routes = [
  { path: '', component: TodoDashboardComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
      TodoDashboardComponent,
      TodoListComponent,
      ToDoTaskComponent,
      TodoListThumbnailComponent], 
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes)
  ]
})
export class DashboardModule { }
