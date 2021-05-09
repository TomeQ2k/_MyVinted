import { TestBed } from '@angular/core/testing';

import { ClientExternalAuthService } from './client-external-auth.service';

describe('ClientExternalAuthServiceService', () => {
  let service: ClientExternalAuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClientExternalAuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
