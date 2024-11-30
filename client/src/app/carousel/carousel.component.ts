import { Component, Input } from '@angular/core';
import { CarImageModel } from '../models/car-image-model';
import { CommonModule } from '@angular/common';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss'
})
export class CarouselComponent {
  @Input() images?: CarImageModel[];
  readonly carouselId = 'carousel-' + uuidv4().toString();
}
