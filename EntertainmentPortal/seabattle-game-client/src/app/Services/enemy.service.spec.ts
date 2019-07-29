import { TestBed } from '@angular/core/testing';

import { EnemyService } from './enemy.service';

describe('EnemyService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnemyService = TestBed.get(EnemyService);
    expect(service).toBeTruthy();
  });
});
