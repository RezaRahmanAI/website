import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, combineLatest, map, of, startWith, shareReplay, catchError } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface CourseFilterValue {
  level?: string;
  price?: string;
  keyword?: string;
}

export class CoursesFacade {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  private readonly catalog$ = this.http.get<any[]>(`${this.baseUrl}/courses`).pipe(
    catchError(() => of(this.placeholderCourses)),
    shareReplay(1)
  );

  courses$(changes$: Observable<CourseFilterValue>, initial: CourseFilterValue) {
    return combineLatest([
      this.catalog$,
      changes$.pipe(startWith(initial))
    ]).pipe(
      map(([courses, filters]) => {
        return courses.filter(course => {
          const matchesLevel = !filters.level || course.level?.toLowerCase() === filters.level.toLowerCase();
          const matchesKeyword = !filters.keyword || course.title.toLowerCase().includes(filters.keyword.toLowerCase());
          return matchesLevel && matchesKeyword;
        });
      })
    );
  }

  courseById(id: string) {
    return this.catalog$.pipe(map(courses => courses.find(course => course.id === id)));
  }

  private readonly placeholderCourses = [
    {
      id: 'full-stack',
      title: 'Full-Stack Engineering Accelerator',
      summary: 'Ship enterprise-grade applications with Angular and ASP.NET Core.',
      description: 'A project-based curriculum covering architecture, security, and DevOps best practices.',
      level: 'Advanced',
      duration: '10 weeks',
      price: 2499,
      instructor: {
        name: 'Avery Morgan',
        title: 'Principal Engineer'
      }
    },
    {
      id: 'product-design',
      title: 'Product Design Studio',
      summary: 'Design systems, motion, and cross-platform experiences with Figma.',
      description: 'Hands-on labs and critiques with industry design leaders.',
      level: 'Intermediate',
      duration: '6 weeks',
      price: 1899,
      instructor: {
        name: 'Jordan Blake',
        title: 'Design Director'
      }
    }
  ];
}
