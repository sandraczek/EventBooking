import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = 'http://localhost:5295/api/students';
  private readonly TOKEN_KEY = 'access_token';

  currentUser = signal<{ isLoggedIn: boolean } | null>(null);

  constructor(private http: HttpClient) {
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
          this.currentUser.set({ isLoggedIn: true });
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
    if (this.isLoggedIn()) {
      this.currentUser.set({ isLoggedIn: true });
    }
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
    const token = this.getToken();
    if (!token) return false;

    const decodedToken = this.decodeToken(token);
    const roleClaim = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    if (Array.isArray(roleClaim)) {
      return roleClaim.includes('Admin');
    }
    return roleClaim === 'Admin';
  }
}
