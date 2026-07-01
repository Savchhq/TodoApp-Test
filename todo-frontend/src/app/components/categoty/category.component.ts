import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../services/category.service';
import { CategoryDto, CreateUpdateCategoryDto } from '../../models/category.model';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit {
  categories: CategoryDto[] = [];
  newCategoryName = '';

  editingCategoryId: string | null = null;
  editingCategoryName = '';

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => this.categories = data,
      error: (err) => console.error('Помилка завантаження:', err)
    });
  }

  addCategory(): void {
    if (!this.newCategoryName.trim()) return;

    const newCategory: CreateUpdateCategoryDto = { name: this.newCategoryName.trim() };

    this.categoryService.createCategory(newCategory).subscribe({
      next: (created) => {
        this.categories.push(created);
        this.newCategoryName = '';
      },
      error: (err) => console.error('Помилка створення:', err)
    });
  }

  startEditCategory(category: CategoryDto): void {
    this.editingCategoryId = category.id;
    this.editingCategoryName = category.name;
  }

  cancelEditCategory(): void {
    this.editingCategoryId = null;
    this.editingCategoryName = '';
  }

  saveEditCategory(category: CategoryDto): void {
    if (!this.editingCategoryName.trim()) return;

    const updatedCategory: CreateUpdateCategoryDto = {
      name: this.editingCategoryName.trim()
    };

    this.categoryService.updateCategory(category.id, updatedCategory).subscribe({
      next: (updated) => {
        category.name = updated.name ?? this.editingCategoryName.trim();
        this.editingCategoryId = null;
        this.editingCategoryName = '';
      },
      error: (err) => console.error('Помилка оновлення:', err)
    });
  }

  deleteCategory(id: string): void {
    this.categoryService.deleteCategory(id).subscribe({
      next: () => {
        this.categories = this.categories.filter(c => c.id !== id);
      },
      error: (err) => console.error('Помилка видалення:', err)
    });
  }
}
