import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AutoBid';

  set activeNavElement(value: 'home' | 'users' | 'offers') {
    this.router.navigate([value]);
  };

  constructor(private readonly router: Router) {
    this.activeNavElement = 'home';
  }
}
