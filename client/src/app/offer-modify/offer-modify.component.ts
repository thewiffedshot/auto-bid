import { Component } from '@angular/core';
import { OfferDetailsComponent } from "../offer-details/offer-details.component";
import { Router } from '@angular/router';
import { CarOfferModel } from '../models/car-offer-model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-offer-modify',
  standalone: true,
  imports: [OfferDetailsComponent],
  templateUrl: './offer-modify.component.html',
  styleUrl: './offer-modify.component.scss'
})
export class OfferModifyComponent {
  offer: CarOfferModel;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient
  ) {
    this.offer = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];    
  }

  onCancelClick(): void {
    this.router.navigate(['/offer', this.offer.id]);
  }

  onSaveClick(): void {
    this.httpClient.put(`/api/CarOffer/${this.offer.id}`, this.offer).subscribe(() => {
      this.router.navigate(['/offer', this.offer.id]);
    });
  }
}
