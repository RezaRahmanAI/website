import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { DashboardFacade } from './dashboard.facade';

@Component({
  standalone: true,
  selector: 'app-dashboard-page',
  imports: [NgIf, NgFor, AsyncPipe],
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardPage {
  protected readonly facade = inject(DashboardFacade);
}
