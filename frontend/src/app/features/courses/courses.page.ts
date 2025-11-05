import { AsyncPipe, NgFor } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CoursesFacade } from './courses.facade';

@Component({
  standalone: true,
  selector: 'app-courses-page',
  imports: [ReactiveFormsModule, NgFor, AsyncPipe, RouterLink],
  templateUrl: './courses.page.html',
  styleUrls: ['./courses.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CoursesPage {
  private readonly fb = inject(FormBuilder);
  protected readonly facade = inject(CoursesFacade);

  readonly filters = this.fb.nonNullable.group({
    level: [''],
    price: [''],
    keyword: ['']
  });

  readonly filteredCourses$ = this.facade.courses$(this.filters.valueChanges, this.filters.value);

  constructor() {
    this.filters.updateValueAndValidity({ emitEvent: true });
  }
}
