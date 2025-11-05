import { Routes } from '@angular/router';

export const COURSES_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./courses.page').then(m => m.CoursesPage)
  },
  {
    path: ':id',
    loadComponent: () => import('./course-detail.page').then(m => m.CourseDetailPage)
  }
];
