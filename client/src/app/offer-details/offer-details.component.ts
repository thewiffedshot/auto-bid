import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CarOfferMake, CarOfferModel, initialCarOfferModel } from '../models/car-offer-model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ImageInputListComponent } from "../image-input-list/image-input-list.component";
import { CarImageModel } from '../models/car-image-model';

@Component({
  selector: 'app-offer-details',
  standalone: true,
  imports: [FormsModule, CommonModule, ImageInputListComponent],
  templateUrl: './offer-details.component.html',
  styleUrl: './offer-details.component.scss'
})
export class OfferDetailsComponent implements OnInit, AfterViewInit {
  @Input() offer!: CarOfferModel;
  @Input() readonly = false;

  @Output() offerChanged: EventEmitter<CarOfferModel> = new EventEmitter<CarOfferModel>();
  
  @ViewChild(ImageInputListComponent) imageInputListComponent!: ImageInputListComponent;
  
  CarOfferMake = CarOfferMake;
  objectKeys = Object.keys;
  
  imagesToAdd?: CarImageModel[];
  imagesToRemove?: CarImageModel[];

  ngOnInit(): void {
    this.offer = this.offer || initialCarOfferModel;
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
}
