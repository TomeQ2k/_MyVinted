import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmptyListInfoComponent } from './empty-list-info.component';

describe('EmptyListInfoComponent', () => {
  let component: EmptyListInfoComponent;
  let fixture: ComponentFixture<EmptyListInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmptyListInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmptyListInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
