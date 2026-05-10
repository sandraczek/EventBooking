import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface EventDto {
  id: string;
  name: string;
  maxParticipants: number;
}

export interface CreateReservationCommand {
  eventId: string;
  studentId: string;
}

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5295/api';

  getEvents(): Observable<EventDto[]> {
    return this.http.get<EventDto[]>(`${this.apiUrl}/events`);
  }

  createReservation(command: CreateReservationCommand): Observable<any> {
    return this.http.post(`${this.apiUrl}/reservations/request`, command);
  }
}
