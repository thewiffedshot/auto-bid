import { AfterViewInit, Component, Input } from '@angular/core';
import { CarOfferModel } from '../models/car-offer-model';
import { CarouselComponent } from "../carousel/carousel.component";
import { HttpClient } from '@angular/common/http';
import { CarImageModel } from '../models/car-image-model';

@Component({
  selector: 'app-offer-listing',
  standalone: true,
  imports: [CarouselComponent],
  templateUrl: './offer-listing.component.html',
  styleUrl: './offer-listing.component.scss'
})
export class OfferListingComponent implements AfterViewInit {
  @Input() offer!: CarOfferModel;
  
  constructor(private readonly httpClient: HttpClient) { }

  ngAfterViewInit(): void {
    if (this.offer.id) {
      this.httpClient.get<CarImageModel[]>(`/api/CarImage/ForOffer/${this.offer.id}`).subscribe(images => {
        this.offer.images = images;
      });    
    }
  }
}
