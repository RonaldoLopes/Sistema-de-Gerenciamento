import { TestBed } from '@angular/core/testing';

import { RelatoriosexcelService } from './relatoriosexcel.service';

describe('RelatoriosexcelService', () => {
  let service: RelatoriosexcelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RelatoriosexcelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
