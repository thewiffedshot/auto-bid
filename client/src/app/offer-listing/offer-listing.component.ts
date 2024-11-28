import { AfterViewInit, Component, Input } from '@angular/core';
import { CarOfferModel } from '../models/car-offer-model';
import { CarouselComponent } from "../carousel/carousel.component";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offer-listing',
  standalone: true,
  imports: [CarouselComponent],
  templateUrl: './offer-listing.component.html',
  styleUrl: './offer-listing.component.scss'
})
export class OfferListingComponent implements AfterViewInit {
  @Input() offer!: CarOfferModel;
  
  constructor(
    private readonly router: Router
  ) { }

  ngAfterViewInit(): void {
    
  }

  openOffer(): void {
    this.router.navigate(['/offer', this.offer.id], { state: { openedOffer: this.offer } });
  }
}
