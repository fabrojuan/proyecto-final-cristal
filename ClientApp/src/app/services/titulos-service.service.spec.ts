import { TestBed } from '@angular/core/testing';

import { TitulosServiceService } from './titulos-service.service';

describe('TitulosServiceService', () => {
  let service: TitulosServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TitulosServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
