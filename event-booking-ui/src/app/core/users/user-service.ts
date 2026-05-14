import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private readonly API_URL = environment.apiUrl + '/api/users';

  deleteUser(userId: string) {
    return this.http.delete(`${this.API_URL}/${userId}`);
  }
  getUsers() {
    return this.http.get<any[]>(this.API_URL);
  }
}
