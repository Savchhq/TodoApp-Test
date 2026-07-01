import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { CategoryService } from '../../services/category.service';
import { TaskDto, UpdateTaskDto } from '../../models/task.model';
import { CategoryDto } from '../../models/category.model';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent implements OnInit {
  tasks: TaskDto[] = [];
  categories: CategoryDto[] = [];
  totalCount = 0;

  newTaskTitle = '';
  newTaskDescription = '';
  selectedCategoryId = '';

  searchQuery = '';
  filterCategoryId = '';
  pageNumber = 1;
  pageSize = 10;

  editingTaskId: string | null = null;
  editTitle = '';
  editDescription = '';
  editCategoryId = '';
  editIsCompleted = false;

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks({
      searchQuery: this.searchQuery,
      categoryId: this.filterCategoryId || undefined,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize
    }).subscribe({
      next: (data: any) => {
        const items = data?.items ?? data?.Items;
        this.tasks = Array.isArray(items) ? items : Array.isArray(data) ? data : [];
        this.totalCount = data?.totalCount ?? data?.TotalCount ?? this.tasks.length;
      },
      error: (err) => console.error('Помилка завантаження завдань:', err)
    });
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
        if (this.categories.length > 0 && !this.selectedCategoryId) {
          this.selectedCategoryId = this.categories[0].id;
        }
      },
      error: (err) => console.error('Помилка завантаження категорій:', err)
    });
  }

  applyFilters(): void {
    this.pageNumber = 1;
    this.loadTasks();
  }

  clearFilters(): void {
    this.searchQuery = '';
    this.filterCategoryId = '';
    this.pageNumber = 1;
    this.loadTasks();
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.pageNumber = page;
    this.loadTasks();
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.totalCount / this.pageSize));
  }

  addTask(): void {
    if (!this.newTaskTitle.trim() || !this.selectedCategoryId) return;

    this.taskService.addTask(
      this.newTaskTitle,
      this.selectedCategoryId,
      this.newTaskDescription.trim() || undefined
    ).subscribe({
      next: () => {
        this.newTaskTitle = '';
        this.newTaskDescription = '';
        this.loadTasks();
      },
      error: (err) => console.error('Помилка при додаванні:', err)
    });
  }

  toggleTask(task: TaskDto): void {
    const updatedTask: UpdateTaskDto = {
      title: task.title,
      description: task.description,
      isCompleted: !task.isCompleted,
      categoryId: task.categoryId
    };

    this.taskService.updateTask(task.id, updatedTask).subscribe({
      next: (updated) => {
        Object.assign(task, updated);
      },
      error: (err) => console.error('Помилка оновлення статусу:', err)
    });
  }

  startEditTask(task: TaskDto): void {
    this.editingTaskId = task.id;
    this.editTitle = task.title;
    this.editDescription = task.description ?? '';
    this.editCategoryId = task.categoryId;
    this.editIsCompleted = task.isCompleted;
  }

  cancelEditTask(): void {
    this.editingTaskId = null;
  }

  saveEditTask(task: TaskDto): void {
    if (!this.editTitle.trim() || !this.editCategoryId) return;

    const updatedTask: UpdateTaskDto = {
      title: this.editTitle.trim(),
      description: this.editDescription.trim() || undefined,
      isCompleted: this.editIsCompleted,
      categoryId: this.editCategoryId
    };

    this.taskService.updateTask(task.id, updatedTask).subscribe({
      next: (updated) => {
        Object.assign(task, updated);
        this.editingTaskId = null;
      },
      error: (err) => console.error('Помилка оновлення завдання:', err)
    });
  }

  deleteTask(id: string): void {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        if (this.tasks.length === 1 && this.pageNumber > 1) {
          this.pageNumber--;
        }
        this.loadTasks();
      },
      error: (err) => console.error('Помилка видалення:', err)
    });
  }
}
