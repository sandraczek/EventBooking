import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ReservationService, EventDto } from './reservation';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private reservationService = inject(ReservationService);
  private fb = inject(FormBuilder);

  events: EventDto[] = [];
  statusMessage = '';
  isError = false;
  isSubmitting = false;

  reservationForm = this.fb.group({
    eventId: ['', Validators.required],
    studentId: ['', [Validators.required, Validators.pattern('^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$')]]
  });

  ngOnInit(): void {
    this.reservationService.getEvents().subscribe({
      next: (data) => this.events = data,
      error: () => this.showMessage('Błąd pobierania wydarzeń z API', true)
    });
  }

  onSubmit(): void {
    if (this.reservationForm.invalid) {
      this.showMessage('Wypełnij poprawnie formularz. Student ID musi być prawidłowym formatem GUID.', true);
      return;
    }

    this.isSubmitting = true;
    this.statusMessage = '';

    this.reservationService.createReservation(this.reservationForm.value as any).subscribe({
      next: () => {
        this.showMessage('Żądanie rezerwacji przyjęte przez serwer w tle!', false);
        this.reservationForm.reset();
        this.isSubmitting = false;
      },
      error: (err) => {
        this.showMessage(`Odrzucono: ${err.error?.detail || err.error?.title || 'Nieznany błąd serwera'}`, true);
        this.isSubmitting = false;
      }
    });
  }

  private showMessage(msg: string, isErr: boolean): void {
    this.statusMessage = msg;
    this.isError = isErr;
  }
}
