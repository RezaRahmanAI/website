import { ChangeDetectionStrategy, Component, effect, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layouts/header.component';
import { FooterComponent } from './layouts/footer.component';
import Lenis from '@studio-freight/lenis';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private lenis = new Lenis({
    duration: 1.1,
    smoothWheel: true
  });

  constructor() {
    effect(() => {
      const raf = (time: number) => {
        this.lenis.raf(time);
        requestAnimationFrame(raf);
      };
      requestAnimationFrame(raf);
    });
  }
}
