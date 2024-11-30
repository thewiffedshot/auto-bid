import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { OfferDetailsComponent } from "../offer-details/offer-details.component";
import { Router } from '@angular/router';
import { CarOfferModel, initialCarOfferModel } from '../models/car-offer-model';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-offer-modify',
  standalone: true,
  imports: [OfferDetailsComponent],
  templateUrl: './offer-modify.component.html',
  styleUrl: './offer-modify.component.scss'
})
export class OfferModifyComponent implements OnDestroy {
  offer: CarOfferModel;
  
  @ViewChild(OfferDetailsComponent) offerDetailsComponent!: OfferDetailsComponent;

  private subscription?: Subscription;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient
  ) {
    this.offer = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];    
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  onCancelClick(): void {
    this.router.navigate(['/offer', this.offer.id]);
  }

  onSaveClick(): void {
    this.offer.carImagesToAdd = this.offerDetailsComponent.imagesToAdd;
    this.offer.carImagesToDelete = this.offerDetailsComponent.imagesToRemove?.map(image => image.id!);

    this.httpClient.put(`/api/CarOffer/${this.offer.id}`, { ...this.offer, images: undefined }).subscribe(() => {
      this.router.navigate(['/offer', this.offer.id], { state: { openedOffer: initialCarOfferModel } });
    });
  }

  onDeleteClick(): void {
    this.httpClient.delete(`/api/CarOffer/${this.offer.id}`).subscribe(() => {
      this.router.navigate(['/dashboard'], { state: { openedOffer: initialCarOfferModel } });
    });
  }
}
