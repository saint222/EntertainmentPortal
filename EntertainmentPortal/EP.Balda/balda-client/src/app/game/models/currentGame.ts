import { Cell } from './cell';

export class CurrentGame {
  userId: string;
  gameId: string;
  mapId: string;
  isGameOver: boolean;
  isPlayersTurn: boolean;
  playerScore: number;
  opponentScore: number;
  cells: Cell[][];
  }
