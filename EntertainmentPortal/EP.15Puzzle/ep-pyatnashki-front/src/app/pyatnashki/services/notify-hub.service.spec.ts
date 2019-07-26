import { TestBed } from '@angular/core/testing';

import { NotifyHubService } from './notify-hub.service';

describe('NotifyHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NotifyHubService = TestBed.get(NotifyHubService);
    expect(service).toBeTruthy();
  });
});
