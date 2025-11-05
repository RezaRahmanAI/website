import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/home/home.page').then(m => m.HomePage)
  },
  {
    path: 'services',
    loadComponent: () => import('./features/services/services.page').then(m => m.ServicesPage)
  },
  {
    path: 'courses',
    loadChildren: () => import('./features/courses/courses.routes').then(m => m.COURSES_ROUTES)
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.DASHBOARD_ROUTES)
  },
  {
    path: 'contact',
    loadComponent: () => import('./features/contact/contact.page').then(m => m.ContactPage)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
