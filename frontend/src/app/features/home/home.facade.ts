import { HttpClient } from '@angular/common/http';
import { computed, inject, signal } from '@angular/core';
import { catchError, map, of, shareReplay } from 'rxjs';
import { environment } from '../../../environments/environment';

export class HomeFacade {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  readonly services$ = this.http.get<any[]>(`${this.baseUrl}/services`).pipe(
    catchError(() => of(this.defaultServices)),
    shareReplay(1)
  );
  readonly courses$ = this.http.get<any[]>(`${this.baseUrl}/courses`).pipe(
    catchError(() => of(this.defaultCourses)),
    shareReplay(1)
  );
  readonly blogPosts$ = this.http.get<any[]>(`${this.baseUrl}/blog`).pipe(
    catchError(() => of(this.defaultPosts)),
    shareReplay(1)
  );

  readonly stats$ = this.courses$.pipe(
    map(courses => ({
      projects: 120,
      students: 3400,
      yearsExperience: 12,
      satisfaction: 98,
      popularCourse: courses[0]?.title ?? 'Full Stack Accelerator'
    }))
  );

  readonly testimonials = signal([
    {
      quote: 'TechNova transformed our digital presence with cutting-edge strategies and flawless execution.',
      author: 'Samantha Rivera',
      role: 'VP Marketing, Horizon Labs'
    },
    {
      quote: 'The academy empowers our team with the skills to innovate faster than ever before.',
      author: 'Michael Chen',
      role: 'Head of Product, BrightWave'
    }
  ]);
  private readonly defaultServices = [
    {
      name: 'Digital Growth Acceleration',
      summary: 'Full-funnel SEO, SEM, and data-driven marketing campaigns.'
    },
    {
      name: 'Experience Engineering',
      summary: 'Human-centered web and product design across platforms.'
    },
    {
      name: 'Cloud Native Delivery',
      summary: 'Resilient, secure applications deployed on modern cloud infrastructure.'
    }
  ];

  private readonly defaultCourses = [
    {
      id: 'ux-strategy',
      title: 'UX Strategy Lab',
      summary: 'Architect delightful user journeys backed by research and data.',
      level: 'Intermediate',
      duration: '6 weeks'
    },
    {
      id: 'ai-marketing',
      title: 'AI for Marketing Leaders',
      summary: 'Operationalize AI to accelerate acquisition and retention.',
      level: 'Advanced',
      duration: '4 weeks'
    }
  ];

  private readonly defaultPosts = [
    {
      title: 'Building Ethical AI Experiences',
      excerpt: 'A blueprint for responsible machine learning adoption.',
      slug: 'ethical-ai-experiences'
    }
  ];
}
