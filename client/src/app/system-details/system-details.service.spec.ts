import { TestBed } from '@angular/core/testing';

import { SystemDetailsService } from './system-details.service';

describe('SystemDetailsService', () => {
  let service: SystemDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SystemDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
