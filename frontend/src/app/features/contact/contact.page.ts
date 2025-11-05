import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  standalone: true,
  selector: 'app-contact-page',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact.page.html',
  styleUrls: ['./contact.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ContactPage {
  private readonly fb = inject(FormBuilder);
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl;

  readonly form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: [''],
    serviceInterest: ['', Validators.required],
    message: ['', Validators.required]
  });

  readonly submitted = this.fb.control(false);

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.http.post(`${this.baseUrl}/contact`, this.form.getRawValue()).subscribe(() => {
      this.submitted.setValue(true);
      this.form.reset();
    });
  }
}
