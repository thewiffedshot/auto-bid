import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OffersDashboardComponent } from './offers-dashboard.component';

describe('OffersDashboardComponent', () => {
  let component: OffersDashboardComponent;
  let fixture: ComponentFixture<OffersDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OffersDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OffersDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
