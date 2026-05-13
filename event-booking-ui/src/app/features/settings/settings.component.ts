import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentProfileComponent } from './components/student-profile/student-profile.component';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, StudentProfileComponent],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  activeTab = signal<'student' | 'general'>('student');
}
