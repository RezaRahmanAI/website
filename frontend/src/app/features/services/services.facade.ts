import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, of, shareReplay } from 'rxjs';
import { environment } from '../../../environments/environment';

export class ServicesFacade {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  readonly services$ = this.http.get<any[]>(`${this.baseUrl}/services`).pipe(
    catchError(() => of(this.placeholderServices)),
    shareReplay(1)
  );

  private readonly placeholderServices = [
    {
      name: 'Digital Marketing Mastery',
      description: 'SEO, SEM, and omni-channel growth campaigns managed end-to-end.',
      process: ['Strategy workshop', 'Campaign launch', 'Continuous optimization'],
      technologies: ['HubSpot', 'GA4', 'Meta Ads', 'Looker'],
      pricing: ['Growth', 'Scale', 'Enterprise']
    },
    {
      name: 'Web Experience Platform',
      description: 'Composable web architecture with Angular and headless CMS integrations.',
      process: ['Discovery sprint', 'Design system', 'Engineering & QA', 'Launch & iterate'],
      technologies: ['Angular', 'ASP.NET Core', 'Azure', 'Contentful'],
      pricing: ['Foundation', 'Experience', 'Signature']
    }
  ];
}
