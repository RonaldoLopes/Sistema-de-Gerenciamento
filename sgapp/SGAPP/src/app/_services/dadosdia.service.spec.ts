import { TestBed } from '@angular/core/testing';

import { DadosdiaService } from './dadosdia.service';

describe('DadosdiaService', () => {
  let service: DadosdiaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DadosdiaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
