import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOfferButtonComponent } from './add-offer-button.component';

describe('AddOfferButtonComponent', () => {
  let component: AddOfferButtonComponent;
  let fixture: ComponentFixture<AddOfferButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOfferButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOfferButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
