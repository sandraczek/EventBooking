import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EventService } from '../event-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-event',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-event.html',
  styleUrls: ['./create-event.scss']
})
export class CreateEvent {
  private fb = inject(FormBuilder);
  private eventService = inject(EventService);
  private router = inject(Router);

  eventForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(5)]],
    description: ['', Validators.required],
    date: ['', Validators.required],
    maxParticipants: [10, [Validators.required, Validators.min(1)]],
    ticketPrice: [10, [Validators.required, Validators.min(0)]]
  });

  onSubmit() {
    if (this.eventForm.invalid) return;

    this.eventService.createEvent(this.eventForm.value).subscribe({
      next: () => {
        alert('Wydarzenie dodane pomyślnie!');
        this.router.navigate(['/reservations']);
      },
      error: (err) => {
        console.error(err);
        alert('Błąd podczas dodawania wydarzenia. Sprawdź konsolę.');
      }
    });
  }
}
