import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import {
  CarOfferMake,
  CarOfferModel,
  initialCarOfferModel,
} from '../models/car-offer-model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ImageInputListComponent } from '../image-input-list/image-input-list.component';
import { CarImageModel } from '../models/car-image-model';
import { Subscription } from 'rxjs';
import { WebSocketService } from '../services/web-socket.service';

@Component({
  selector: 'app-offer-details',
  standalone: true,
  imports: [FormsModule, CommonModule, ImageInputListComponent],
  providers: [WebSocketService],
  templateUrl: './offer-details.component.html',
  styleUrl: './offer-details.component.scss',
})
export class OfferDetailsComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() offer!: CarOfferModel;
  @Input() readonly = false;

  @Output() offerChanged: EventEmitter<CarOfferModel> =
    new EventEmitter<CarOfferModel>();

  @ViewChild(ImageInputListComponent)
  imageInputListComponent!: ImageInputListComponent;

  CarOfferMake = CarOfferMake;
  objectKeys = Object.keys;

  imagesToAdd?: CarImageModel[];
  imagesToRemove?: CarImageModel[];

  private messageSubscription: Subscription = new Subscription();

  constructor(private webSocketService: WebSocketService) {}

  ngOnInit(): void {
    this.offer = this.offer || initialCarOfferModel;

    setInterval(() => {
      this.sendMessage(); 
    }, 1000);
  }

  ngAfterViewInit(): void {
    this.imagesChanged();
  }

  imagesChanged(): void {
    if (!this.imageInputListComponent) {
      return;
    }

    this.imagesToAdd = this.imageInputListComponent.imagesToAdd;
    this.imagesToRemove = this.imageInputListComponent.imagesToRemove;
  }

  sendMessage() {
    if (!this.offer || !this.offer.id) {
      console.error('Offer ID is not set. Cannot send message.');
      return;
    }

    const message = this.offer.id + '2';
    this.webSocketService.sendMessage(message);
  }

  ngOnDestroy() {
    // Unsubscribe from WebSocket messages and close the connection
    this.messageSubscription.unsubscribe();
    this.webSocketService.closeConnection();
  }
}
