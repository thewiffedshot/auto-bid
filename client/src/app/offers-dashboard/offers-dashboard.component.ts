import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CarOfferModel } from '../models/car-offer-model';
import { OfferListingComponent } from "../offer-listing/offer-listing.component";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { forkJoin, map, merge, mergeMap } from 'rxjs';
import { CarImageModel } from '../models/car-image-model';
import { environment } from '../../environments/environment';

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
  ) {
    this.httpClient.get<CarOfferModel[]>(`${environment.apiUrl}/api/CarOffer`).pipe(
      map(offers => {
        const imageRequests = offers.map(offer => 
          this.httpClient.get<CarImageModel[]>(`${environment.apiUrl}/api/CarImage/ForOffer/${offer.id}`).pipe(
            map(images => ({ ...offer, images }))
          )
        );
        return imageRequests;
      }),
      mergeMap(requests => forkJoin(requests))
    ).subscribe(offersWithImages => {
      this.offers = offersWithImages;
    });
  }
}
