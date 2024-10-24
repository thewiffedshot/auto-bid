import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-offers-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './offers-dashboard.component.html',
  styleUrl: './offers-dashboard.component.scss'
})
export class OffersDashboardComponent {

}
