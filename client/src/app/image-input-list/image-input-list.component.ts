import { Component } from '@angular/core';
import { CarImageModel } from '../models/car-image-model';

@Component({
  selector: 'app-image-input-list',
  standalone: true,
  imports: [],
  templateUrl: './image-input-list.component.html',
  styleUrl: './image-input-list.component.scss'
})
export class ImageInputListComponent {
  public images: CarImageModel[] = [];
  
  public addImage(image: CarImageModel): void {
    this.images.push(image);
  }

  public removeImage(index: number): void {
    this.images.splice(index, 1);
  }

  public onFileChange(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    const files = fileInput.files as unknown as File[];

    const reader = new FileReader();

    reader.onload = (e: ProgressEvent) => {
      const image = new CarImageModel();
      image.base64ImageData = reader.result as string;
      this.addImage(image);
    };

    files.forEach(file => {
      reader.readAsDataURL(file);
    });
  }
}
