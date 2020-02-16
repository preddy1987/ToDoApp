import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToDoTaskComponent } from './todo-task.component';

describe('TodoItemComponent', () => {
  let component: ToDoTaskComponent;
  let fixture: ComponentFixture<ToDoTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToDoTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToDoTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
