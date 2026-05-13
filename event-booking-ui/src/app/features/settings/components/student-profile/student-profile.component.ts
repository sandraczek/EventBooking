import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { StudentApiService } from '../../../../core/api/student-api.service';
import { AuthService } from '../../../../core/auth/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.scss']
})
export class StudentProfileComponent {
  private studentApi = inject(StudentApiService);
  private authService = inject(AuthService);
  private fb = inject(FormBuilder);

  isEmailSending = signal(false);
  emailMessage = signal<string | null>(null);
  isError = signal(false);

  // Formularz dla przyszłego numeru indeksu (na razie wyłączony)
  studentForm = this.fb.group({
    indexNumber: [{ value: '', disabled: true }, [Validators.required, Validators.pattern('^[0-9]{5,6}$')]]
  });

  sendVerificationEmail() {
    this.isEmailSending.set(true);
    this.emailMessage.set(null);
    this.isError.set(false);

    // Wywołujemy strzał do backendu bez podawania ID.
    // Twój HttpInterceptor dorzuci nagłówek z tokenem.
    this.studentApi.sendConfirmationEmail().subscribe({
      next: () => {
        this.isEmailSending.set(false);
        this.emailMessage.set('Wysłano! Sprawdź swoją skrzynkę pocztową.');
      },
      error: (err) => {
        this.isError.set(true);
        this.isEmailSending.set(false);
        this.emailMessage.set('Wystąpił błąd serwera. Spróbuj ponownie później.');
        console.error('Błąd weryfikacji:', err);
      }
    });
  }
}
