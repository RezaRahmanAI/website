import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { CoursesFacade } from './courses.facade';

@Component({
  standalone: true,
  selector: 'app-course-detail-page',
  imports: [NgIf, NgFor, AsyncPipe, RouterLink],
  templateUrl: './course-detail.page.html',
  styleUrls: ['./course-detail.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CourseDetailPage {
  private readonly route = inject(ActivatedRoute);
  private readonly facade = inject(CoursesFacade);

  readonly course$ = this.route.paramMap.pipe(
    switchMap(params => this.facade.courseById(params.get('id') ?? ''))
  );
}
