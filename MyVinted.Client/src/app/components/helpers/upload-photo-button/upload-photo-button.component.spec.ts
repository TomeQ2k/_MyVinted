import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadPhotoButtonComponent } from './upload-photo-button.component';

describe('UploadPhotoButtonComponent', () => {
  let component: UploadPhotoButtonComponent;
  let fixture: ComponentFixture<UploadPhotoButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadPhotoButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadPhotoButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
