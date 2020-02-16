export interface ToDoList {
    id: number;
    title: string;
    description: string;
    tasks: Task[];
  }

export interface Task {
    title: string;
  }