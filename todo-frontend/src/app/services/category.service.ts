import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// Імпортуємо обидві твої моделі
import { CategoryDto, CreateUpdateCategoryDto } from '../models/category.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = `${environment.apiUrl}/Task`;

  constructor(private http: HttpClient) { }

  // 1. Отримати всі категорії (повертає масив CategoryDto з ID)
  getCategories(): Observable<CategoryDto[]> {
    return this.http.get<CategoryDto[]>(this.apiUrl);
  }

  // 2. Створити нову категорію (приймає CreateUpdateCategoryDto без ID, повертає готову CategoryDto з ID)
  createCategory(category: CreateUpdateCategoryDto): Observable<CategoryDto> {
    return this.http.post<CategoryDto>(this.apiUrl, category);
  }

  // 3. Оновити існуючу категорію
  updateCategory(id: string, category: CreateUpdateCategoryDto): Observable<CategoryDto> {
    return this.http.put<CategoryDto>(`${this.apiUrl}/${id}`, category);
  }

  // 4. Видалити категорію
  deleteCategory(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}