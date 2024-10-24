import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { OffersDashboardComponent } from './offers-dashboard/offers-dashboard.component';
import { HomeComponent } from './home/home.component';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, OffersDashboardComponent, HomeComponent, UsersDashboardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'client';

  activeNavElement: 'home' | 'users' | 'offers' = 'home';
}
