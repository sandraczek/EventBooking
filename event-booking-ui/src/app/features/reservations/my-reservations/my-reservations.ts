import { Component, inject, OnInit, signal } from '@angular/core';
import { ReservationDto, ReservationService } from '../reservation-service';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './my-reservations.html',
  styleUrls: ['./my-reservations.scss']
})
export class MyReservations implements OnInit {
  private reservationService = inject(ReservationService);

  reservations = signal<ReservationDto[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  ngOnInit() {
    this.fetchReservations();
  }

  fetchReservations() {
    this.isLoading.set(true);
    this.error.set(null);

    this.reservationService.getMyReservations().subscribe({
      next: (data) => {
        this.reservations.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Błąd pobierania rezerwacji:', err);
        this.error.set('Nie udało się pobrać Twoich rezerwacji. Spróbuj ponownie później.');
        this.isLoading.set(false);
      }
    });
  }

  cancelReservation(id: string) {
    alert(`Funkcja anulowania rezerwacji (${id}) wkrótce dostępna!`);
  }
}
