import { Component, Input } from '@angular/core';
import { CarOfferModel } from '../models/car-offer-model';
import { CarouselComponent } from "../carousel/carousel.component";

@Component({
  selector: 'app-offer-listing',
  standalone: true,
  imports: [CarouselComponent],
  templateUrl: './offer-listing.component.html',
  styleUrl: './offer-listing.component.scss'
})
export class OfferListingComponent {
  @Input() offer!: CarOfferModel;
}
