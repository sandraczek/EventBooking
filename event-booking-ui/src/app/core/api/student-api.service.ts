import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class StudentApiService {
  private http = inject(HttpClient);
  // Ścieżka bazowa na razie z palca, zrób z tym porządek (environments) w przyszłości
  private readonly baseUrl = 'http://localhost:5295/api/students';

  sendConfirmationEmail(): Observable<void> {
    // Interceptor sam dołoży nagłówek Authorization z tokenem
    return this.http.post<void>(`${this.baseUrl}/send-confirmation`, {});
  }
}
