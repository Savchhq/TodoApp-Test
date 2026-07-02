import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetTasksQuery, TaskDto, TasksPagedResult, UpdateTaskDto } from '../models/task.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/Task`;

  constructor(private http: HttpClient) { }

  getTasks(query?: GetTasksQuery): Observable<TasksPagedResult> {
    let params = new HttpParams();

    if (query?.searchQuery?.trim()) {
      params = params.set('searchQuery', query.searchQuery.trim());
    }
    if (query?.categoryId) {
      params = params.set('categoryId', query.categoryId);
    }
    if (query?.pageNumber) {
      params = params.set('pageNumber', query.pageNumber.toString());
    }
    if (query?.pageSize) {
      params = params.set('pageSize', query.pageSize.toString());
    }

    return this.http.get<TasksPagedResult>(this.apiUrl, { params });
  }

  addTask(title: string, categoryId: string, description?: string): Observable<TaskDto> {
    return this.http.post<TaskDto>(this.apiUrl, {
      title,
      categoryId,
      description
    });
  }

  updateTask(id: string, task: UpdateTaskDto): Observable<TaskDto> {
    return this.http.put<TaskDto>(`${this.apiUrl}/${id}`, task);
  }

  deleteTask(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
