import { Component, Input } from '@angular/core';
import { CarOfferMake, CarOfferModel } from '../models/car-offer-model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-offer-details',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './offer-details.component.html',
  styleUrl: './offer-details.component.scss'
})
export class OfferDetailsComponent {
  @Input() offer!: CarOfferModel;
  @Input() readonly = false;

  CarOfferMake = CarOfferMake;
  objectKeys = Object.keys;
}
