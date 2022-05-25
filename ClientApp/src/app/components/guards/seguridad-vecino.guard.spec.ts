import { TestBed } from '@angular/core/testing';

import { SeguridadVecinoGuard } from './seguridad-vecino.guard';

describe('SeguridadVecinoGuard', () => {
  let guard: SeguridadVecinoGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SeguridadVecinoGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
