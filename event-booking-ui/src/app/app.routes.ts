import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Reservations } from './features/reservations/reservations';
import { CreateEvent } from './features/events/create-event/create-event';
import { authGuard } from './core/auth/auth-guard';
import { adminGuard } from './core/auth/admin-guard';
import { UserList } from './core/users/user-list/user-list';
import { ConfirmStudentEmailComponent } from './features/auth/confirm-student-email/confirm-email.component';
import {SettingsComponent} from './features/settings/settings.component'

export const routes: Routes = [
  {
    path: 'settings',
    component: SettingsComponent,
    canActivate: [authGuard]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'reservations', component: Reservations, canActivate: [authGuard] },
  {
    path: 'add-event',
    component: CreateEvent,
    canActivate: [authGuard, adminGuard]
  },
  {
    path: 'users',
    component: UserList,
    canActivate: [authGuard, adminGuard]
  },
  {
    path: 'api/students/confirm-email',
    component : ConfirmStudentEmailComponent
  }

];
