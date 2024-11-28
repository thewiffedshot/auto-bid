import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CarImageModel } from '../models/car-image-model';

@Component({
  selector: 'app-image-input-list',
  standalone: true,
  imports: [],
  templateUrl: './image-input-list.component.html',
  styleUrl: './image-input-list.component.scss'
})
export class ImageInputListComponent {
  @Input() images: CarImageModel[] = [];
  @Input() readonly = false;

  @Output() imagesChanged: EventEmitter<void> = new EventEmitter<void>();
  
  imagesToRemove: CarImageModel[] = [];
  imagesToAdd: CarImageModel[] = [];
  
  public addImage(image: CarImageModel): void {
    this.images.push(image);
    this.imagesToAdd.push(image);
  }

  public removeImage(index: number): void {
    let removedImage = this.images.splice(index, 1)[0];

    if (removedImage.id) {
      this.imagesToRemove.push({id: removedImage.id});
    } else {
      this.imagesToAdd = this.imagesToAdd.filter(image => image !== removedImage);
    }
  }

  public onFileChange(event: Event): void {
    const fileInput = event.currentTarget as HTMLInputElement;
    const files = fileInput.files as unknown as File[];

    const reader = new FileReader();

    reader.onload = (e: ProgressEvent) => {
      const image = new CarImageModel();
      image.base64ImageData = (reader.result as string).split(',')[1];
      this.addImage(image);
    };

    if (files.length) {
      reader.readAsDataURL(files[files.length - 1]);
    }
  }
}
