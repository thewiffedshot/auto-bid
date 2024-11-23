import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferModifyComponent } from './offer-modify.component';

describe('OfferModifyComponent', () => {
  let component: OfferModifyComponent;
  let fixture: ComponentFixture<OfferModifyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfferModifyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
