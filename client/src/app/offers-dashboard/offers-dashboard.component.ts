import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CarOfferModel } from '../models/car-offer-model';
import { OfferListingComponent } from "../offer-listing/offer-listing.component";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offers-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, OfferListingComponent],
  templateUrl: './offers-dashboard.component.html',
  styleUrl: './offers-dashboard.component.scss'
})
export class OffersDashboardComponent {
  @Input() offers: CarOfferModel[] = [];

  constructor(
    private readonly httpClient: HttpClient, 
    private readonly router: Router
  ) {
    this.httpClient.get<CarOfferModel[]>('/api/CarOffer').subscribe(offers => {
      this.offers = offers;
    });
  }

  public openOffer(offer: CarOfferModel) {
    this.router.navigate(['/offer', offer.id], { state: { openedOffer: offer } });
  }
}
