import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CarOfferModel } from '../models/car-offer-model';
import { OfferListingComponent } from "../offer-listing/offer-listing.component";
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, mergeMap } from 'rxjs';
import { CarImageModel } from '../models/car-image-model';
import { environment } from '../../environments/environment';
import { OnInit } from '@angular/core';
import { ILoginService } from '../services/login.service.interface';
import { LoginServiceMock } from '../services/login.service';

@Component({
  selector: 'app-offers-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, OfferListingComponent],
  providers: [
    { provide: ILoginService, useClass: LoginServiceMock } // Replace with actual implementation
  ],
  templateUrl: './offers-dashboard.component.html',
  styleUrl: './offers-dashboard.component.scss'
})
export class OffersDashboardComponent implements OnInit {
  @Input() offers: CarOfferModel[] = [];

  constructor(
    private readonly httpClient: HttpClient, 
    private readonly loginService: ILoginService
  ) { }

  private loadOffers(): void {
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

  ngOnInit(): void {
    this.loginService.user$?.subscribe(user => {
      if (user) {
        console.log(`User logged in: ${user}`);
        this.loadOffers(); // Load offers only if a user is logged in         
      } else {
        this.offers = []; // Clear offers if no user is logged in
        console.log('No user logged in');
      }
    });
  }
}
