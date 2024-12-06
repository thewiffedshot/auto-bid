import { Component, OnDestroy, OnInit } from '@angular/core';
import { OfferDetailsComponent } from "../offer-details/offer-details.component";
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, map, mergeMap, Subscription } from 'rxjs';
import { CarOfferModel } from '../models/car-offer-model';
import { HttpClient } from '@angular/common/http';
import { CarImageModel } from '../models/car-image-model';
import { environment } from '../../environments/environment';

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
  ) {
    this.offer = this.router.getCurrentNavigation()?.extras.state?.['openedOffer'];
  }
  
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  ngOnInit(): void {
    if (!this.offer) {
      this.subscription = this.route.params.subscribe(params => {
        if (!params['id']) {
          this.router.navigate(['/']);
          return;
        }

        this.httpClient.get<CarOfferModel>(`${environment.apiUrl}/api/CarOffer/${params['id']}`).pipe(
          map(offer => {
            const imageRequest = this.httpClient.get<CarImageModel[]>(`${environment.apiUrl}/api/CarImage/ForOffer/${offer.id}`).pipe(
              map(images => ({ ...offer, images }))
            );

            return imageRequest;
          }),
          mergeMap(request => request)
        ).subscribe(offer => {
          this.offer = offer;
        });
      });
    }
  }

  onEditClick(): void {
    this.route.params.subscribe(params => {
      this.router.navigate(['/offer/modify', params['id']], { state: { openedOffer: this.offer } });
    });
  }

  onCancelClick(): void {
    this.router.navigate(['/dashboard']);
  }
}
