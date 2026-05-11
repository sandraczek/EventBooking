import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Reservations } from './features/reservations/reservations';
import { CreateEvent } from './features/events/create-event/create-event';
import { authGuard } from './core/auth/auth-guard';
import { adminGuard } from './core/auth/admin-guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'reservations', component: Reservations, canActivate: [authGuard] },
  {
    path: 'add-event',
    component: CreateEvent,
    canActivate: [authGuard, adminGuard]
  }
];
