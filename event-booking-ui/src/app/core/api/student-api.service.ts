import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class StudentApiService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/api/students';

  sendConfirmationEmail(): Observable<void> {
    // Interceptor sam dołoży nagłówek Authorization z tokenem
    return this.http.post<void>(`${this.baseUrl}/send-confirmation`, {});
  }
}
