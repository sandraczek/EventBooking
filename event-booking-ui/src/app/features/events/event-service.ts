import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private http = inject(HttpClient);
  private readonly API_URL = 'http://localhost:5295/api/events';

  createEvent(eventData: any) {
    return this.http.post(`${this.API_URL}/create`, eventData);
  }

  getEvents() {
    return this.http.get<any[]>(this.API_URL);
  }
}
