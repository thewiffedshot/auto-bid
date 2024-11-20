import { Component, Input } from '@angular/core';
import { CarImageModel } from '../models/car-image-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss'
})
export class CarouselComponent {
  @Input() images?: CarImageModel[];
}
