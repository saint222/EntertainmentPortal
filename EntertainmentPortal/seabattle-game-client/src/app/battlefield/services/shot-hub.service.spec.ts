import { TestBed } from '@angular/core/testing';

import { ShotHubService } from './shot-hub.service';

describe('ShotHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ShotHubService = TestBed.get(ShotHubService);
    expect(service).toBeTruthy();
  });
});
