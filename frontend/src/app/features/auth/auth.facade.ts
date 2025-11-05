import { HttpClient } from '@angular/common/http';
import { inject, signal } from '@angular/core';
import { tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface LoginPayload {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  expiresAt: string;
  userId: string;
  email: string;
  fullName: string;
  role: string;
}

export class AuthFacade {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;
  readonly currentUser = signal<AuthResponse | null>(null);

  login(payload: LoginPayload) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/auth/login`, payload).pipe(
      tap(response => this.currentUser.set(response))
    );
  }

  register(payload: any) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/auth/register`, payload).pipe(
      tap(response => this.currentUser.set(response))
    );
  }
}
