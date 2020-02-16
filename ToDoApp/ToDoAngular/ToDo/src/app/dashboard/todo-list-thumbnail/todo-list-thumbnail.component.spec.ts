import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TodoListThumbnailComponent } from './todo-list-thumbnail.component';

describe('TodoListThumbnailComponent', () => {
  let component: TodoListThumbnailComponent;
  let fixture: ComponentFixture<TodoListThumbnailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TodoListThumbnailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TodoListThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
