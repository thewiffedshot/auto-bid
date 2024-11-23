import { Component, OnDestroy, OnInit } from '@angular/core';
import { OfferDetailsComponent } from "../offer-details/offer-details.component";
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CarOfferModel } from '../models/car-offer-model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-offer-view',
  standalone: true,
  imports: [OfferDetailsComponent],
  templateUrl: './offer-view.component.html',
  styleUrl: './offer-view.component.scss'
})
export class OfferViewComponent implements OnInit, OnDestroy {
  public offer?: CarOfferModel;
  
  private subscription!: Subscription;

  constructor(
    private readonly router: Router,
    private readonly httpClient: HttpClient,
    private readonly route: ActivatedRoute
  ) {}
  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.offer = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];

    if (!this.offer) {
      this.subscription = this.route.params.subscribe(params => {
        if (!params['id']) {
          this.router.navigate(['/']);
          return;
        }

        this.httpClient.get<CarOfferModel>(`/api/CarOffer/${params['id']}`).subscribe(offer => {
          this.offer = offer;
        });
      });
    }
  }
}
