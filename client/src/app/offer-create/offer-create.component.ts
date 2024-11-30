import { Component, ViewChild } from '@angular/core';
import { CarOfferModel, initialCarOfferModel } from '../models/car-offer-model';
import { OfferDetailsComponent } from '../offer-details/offer-details.component';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-offer-create',
  standalone: true,
  imports: [OfferDetailsComponent],
  templateUrl: './offer-create.component.html',
  styleUrl: './offer-create.component.scss'
})
export class OfferCreateComponent {
  private _offer!: CarOfferModel;
  
  get offer(): CarOfferModel {
    return this._offer || initialCarOfferModel;
  }

  set offer(value: CarOfferModel) {
    this._offer = value;
  }
  
  @ViewChild(OfferDetailsComponent) offerDetailsComponent!: OfferDetailsComponent;

  private subscription?: Subscription;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient
  ) {
    this._offer = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];    
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onCancelClick(): void {
    this.router.navigate(['/dashboard']);
  }

  onSaveClick(): void {
    this.offer.carImagesToAdd = this.offerDetailsComponent.imagesToAdd;

    this.httpClient.post<string>(`/api/CarOffer`, { ...this.offer, ownerUsername: 'resonate', images: undefined }).subscribe(offerId => {
      this.router.navigate(['/offer', offerId], { state: { openedOffer: initialCarOfferModel } });
    });
  }
}
