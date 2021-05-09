import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NumberBadgeComponent } from './number-badge.component';

describe('NumberBadgeComponent', () => {
  let component: NumberBadgeComponent;
  let fixture: ComponentFixture<NumberBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NumberBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NumberBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
