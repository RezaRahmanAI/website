import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, ArrowRight, Star, Rocket, GraduationCap } from 'lucide-angular';
import { HomeFacade } from './home.facade';

@Component({
  standalone: true,
  selector: 'app-home-page',
  imports: [NgIf, NgFor, AsyncPipe, RouterLink, LucideAngularModule],
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomePage {
  protected readonly facade = inject(HomeFacade);
  readonly ArrowRight = ArrowRight;
  readonly Star = Star;
  readonly Rocket = Rocket;
  readonly GraduationCap = GraduationCap;
}
