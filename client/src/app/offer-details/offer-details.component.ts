import { AfterViewInit, Component, Input, ViewChild } from '@angular/core';
import { CarOfferMake, CarOfferModel } from '../models/car-offer-model';
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
export class OfferDetailsComponent implements AfterViewInit {
  @Input() offer!: CarOfferModel;
  @Input() readonly = false;
  
  @ViewChild(ImageInputListComponent) imageInputListComponent!: ImageInputListComponent;
  
  CarOfferMake = CarOfferMake;
  objectKeys = Object.keys;
  
  imagesToAdd?: CarImageModel[];
  imagesToRemove?: CarImageModel[];

  ngAfterViewInit(): void {
    this.imagesChanged();
  }
  
  imagesChanged(): void {
    this.imagesToAdd = this.imageInputListComponent.imagesToAdd;
    this.imagesToRemove = this.imageInputListComponent.imagesToRemove;
  }
}
