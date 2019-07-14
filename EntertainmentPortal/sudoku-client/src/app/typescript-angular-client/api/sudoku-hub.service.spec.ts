import { TestBed } from '@angular/core/testing';

import { SudokuHubService } from './sudoku-hub.service';

describe('SudokuHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SudokuHubService = TestBed.get(SudokuHubService);
    expect(service).toBeTruthy();
  });
});
