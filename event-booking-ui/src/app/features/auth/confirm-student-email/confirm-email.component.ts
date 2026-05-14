import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  imports: [RouterLink],
  templateUrl: 'confirm-email.component.html',
  styleUrls: ['confirm-email.component.scss']
})
export class ConfirmStudentEmailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private http = inject(HttpClient);

  status = signal<'loading' | 'success' | 'error'>('loading');

  ngOnInit() {
    const userId = this.route.snapshot.queryParamMap.get('userId');
    const token = this.route.snapshot.queryParamMap.get('token');

    if (!userId || !token) {
      this.status.set('error');
      return;
    }

    this.http.get(environment.apiUrl + `/api/students/confirm-email`, {
      params: { userId, token }
    }).subscribe({
      next: () => this.status.set('success'),
      error: () => this.status.set('error')
    });
  }
}
