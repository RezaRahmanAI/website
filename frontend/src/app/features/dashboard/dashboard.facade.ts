import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, of, shareReplay } from 'rxjs';
import { environment } from '../../../environments/environment';

export class DashboardFacade {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  readonly stats$ = this.http.get<any>(`${this.baseUrl}/admin/dashboard/stats`).pipe(
    catchError(() => of({
      totalUsers: 0,
      totalCourses: 0,
      totalEnrollments: 0,
      totalRevenue: 0
    })),
    shareReplay(1)
  );

  readonly enrollments$ = this.http.get<any>(`${this.baseUrl}/students/dashboard`).pipe(
    catchError(() => of({ enrollments: [], notifications: [], certificates: [] })),
    shareReplay(1)
  );
}
