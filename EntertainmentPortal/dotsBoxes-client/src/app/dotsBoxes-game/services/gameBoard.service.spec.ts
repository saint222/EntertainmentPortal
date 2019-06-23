import { TestBed } from '@angular/core/testing';

import { GameBoardService } from './gameBoard.service';

describe('GameBoardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GameBoardService = TestBed.get(GameBoardService);
    expect(service).toBeTruthy();
  });
});
