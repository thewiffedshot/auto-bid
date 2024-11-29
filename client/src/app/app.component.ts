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

  set activeNavElement(value: 'home' | 'users' | 'offers' | 'createOffer' | 'createUser') {
    let targetRoute = '';

    switch (value) {
      case 'home':
        targetRoute = '/dashboard';
        break;
      case 'users':
        targetRoute = '/users';
        break;
      case 'offers':
        targetRoute = '/offers';
        break;
      case 'createOffer':
        targetRoute = '/offer/create';
        break;
      case 'createUser':
        targetRoute = '/user/create';
        break;
    }

    this.router.navigate([targetRoute]);
  };

  constructor(private readonly router: Router) {
    this.activeNavElement = 'home';
  }
}
