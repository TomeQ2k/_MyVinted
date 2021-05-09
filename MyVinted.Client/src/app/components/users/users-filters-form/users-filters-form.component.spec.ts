import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersFiltersFormComponent } from './users-filters-form.component';

describe('UsersFiltersFormComponent', () => {
  let component: UsersFiltersFormComponent;
  let fixture: ComponentFixture<UsersFiltersFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersFiltersFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersFiltersFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
