import { TestBed } from '@angular/core/testing';

import { SeguridadGuard } from './seguridad.guard';

describe('SeguridadGuard', () => {
  let guard: SeguridadGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SeguridadGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
