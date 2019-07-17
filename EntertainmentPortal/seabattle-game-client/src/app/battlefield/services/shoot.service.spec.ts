import { TestBed } from '@angular/core/testing';

import { ShootService } from './shoot.service';

describe('ShootService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ShootService = TestBed.get(ShootService);
    expect(service).toBeTruthy();
  });
});
