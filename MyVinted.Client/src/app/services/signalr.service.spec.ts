import { TestBed } from '@angular/core/testing';

import { Signalr } from './signalr.service';

describe('SignalrService', () => {
  let service: Signalr;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Signalr);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
