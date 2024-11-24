import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImageInputListComponent } from './image-input-list.component';

describe('ImageInputListComponent', () => {
  let component: ImageInputListComponent;
  let fixture: ComponentFixture<ImageInputListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImageInputListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImageInputListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
