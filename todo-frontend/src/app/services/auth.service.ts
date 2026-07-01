import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LoginResponseDto } from '../models/auth.model';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}

  register(data: any) {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  login(data: any) {
    return this.http.post<LoginResponseDto>(`${this.apiUrl}/login`, data).pipe(
      tap(response => {
        localStorage.setItem('jwtToken', response.jwtToken);
      })
    );
  }

  logout() {
    localStorage.removeItem('jwtToken');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('jwtToken');
  }
}