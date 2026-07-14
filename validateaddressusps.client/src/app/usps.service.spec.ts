import { TestBed } from '@angular/core/testing';

import { Usps } from './usps';

describe('Usps', () => {
  let service: Usps;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Usps);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
