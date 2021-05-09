import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PremiumStatsComponent } from './premium-stats.component';

describe('PremiumStatsComponent', () => {
  let component: PremiumStatsComponent;
  let fixture: ComponentFixture<PremiumStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PremiumStatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
