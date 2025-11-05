import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter, withInMemoryScrolling } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { provideRouterStore } from '@ngrx/router-store';
import { AppComponent } from './app/app.component';
import { appRoutes } from './app/app.routes';
import { HttpErrorInterceptor } from './app/core/interceptors/http-error.interceptor';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withInterceptors([HttpErrorInterceptor.create()])),
    provideRouter(appRoutes, withInMemoryScrolling({ scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled' })),
    provideAnimations(),
    provideStore(),
    provideEffects(),
    provideRouterStore()
  ]
}).catch(err => console.error(err));
