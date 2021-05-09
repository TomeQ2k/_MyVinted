import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LettersLimiterComponent } from './letters-limiter.component';

describe('LettersLimiterComponent', () => {
  let component: LettersLimiterComponent;
  let fixture: ComponentFixture<LettersLimiterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LettersLimiterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LettersLimiterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
