import { Component } from '@angular/core';
import { OffersDashboardComponent } from "../offers-dashboard/offers-dashboard.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [OffersDashboardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
