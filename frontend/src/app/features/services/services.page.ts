import { AsyncPipe, NgFor } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ServicesFacade } from './services.facade';

@Component({
  standalone: true,
  selector: 'app-services-page',
  imports: [NgFor, AsyncPipe],
  templateUrl: './services.page.html',
  styleUrls: ['./services.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ServicesPage {
  protected readonly facade = inject(ServicesFacade);
}
