import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface EventDto {
  id: string;
  name: string;
  maxParticipants: number;
}

export interface ReservationDto {
  reservationId: string;
  eventId: string;
  eventTitle: string;
  eventDate: string;
  status: string;
  createdAt: string;
}

export interface CreateReservationRequest {
  eventId: string;
}

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl + '/api';

  getEvents(): Observable<EventDto[]> {
    return this.http.get<EventDto[]>(`${this.apiUrl}/events`);
  }

  createReservation(request: CreateReservationRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/reservations/request`, request);
  }

  getMyReservations(): Observable<ReservationDto[]> {
    return this.http.get<ReservationDto[]>(`${this.apiUrl}/reservations`);
  }
}
