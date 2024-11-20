import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { mockCarOffer } from '../../mocks/car-offer.mocks';
import { CarOfferModel } from '../models/car-offer-model';
import { OfferListingComponent } from "../offer-listing/offer-listing.component";

@Component({
  selector: 'app-offers-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, OfferListingComponent],
  templateUrl: './offers-dashboard.component.html',
  styleUrl: './offers-dashboard.component.scss'
})
export class OffersDashboardComponent {
  @Input() offers: CarOfferModel[] = [mockCarOffer];
}
