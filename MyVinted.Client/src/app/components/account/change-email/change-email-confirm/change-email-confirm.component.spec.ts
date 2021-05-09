import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeEmailConfirmComponent } from './change-email-confirm.component';

describe('ChangeEmailConfirmComponent', () => {
  let component: ChangeEmailConfirmComponent;
  let fixture: ComponentFixture<ChangeEmailConfirmComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChangeEmailConfirmComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeEmailConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
