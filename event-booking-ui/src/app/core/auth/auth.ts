import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

export interface User {
  id: string;
  email: string;
  role: string | string[];
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);

  private readonly API_URL = environment.apiUrl + '/api/students';
  private readonly TOKEN_KEY = 'access_token';

  currentUser = signal<User | null>(null);

  constructor() {
    this.checkToken();
  }

  register(studentData: any) {
    return this.http.post(`${this.API_URL}/register`, studentData);
  }

  login(credentials: any) {
    return this.http.post<{ accessToken: string }>(`${this.API_URL}/login`, credentials)
      .pipe(
        tap(response => {
          localStorage.setItem(this.TOKEN_KEY, response.accessToken);
          this.setUserFromToken(response.accessToken);
        })
      );
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
    this.currentUser.set(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  private checkToken() {
    const token = this.getToken();
    if (token) {
      this.setUserFromToken(token);
    }
  }

  private setUserFromToken(token: string) {
    const decoded = this.decodeToken(token);
    if (!decoded) return;

    this.currentUser.set({
      id: decoded.sub, // sub pochodzi z JwtProvider.cs
      email: decoded.email,
      role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || []
    });
  }

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch (e) {
      return null;
    }
  }

  isAdmin(): boolean {
    const user = this.currentUser();
    if (!user) return false;

    if (Array.isArray(user.role)) {
      return user.role.includes('Admin');
    }
    return user.role === 'Admin';
  }
}
