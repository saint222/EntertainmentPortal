import { Tile } from './tile';
export class Deck {
  size: number;
  score: number;
  victory: boolean;
  tiles: Tile[];

  constructor(score: number, size: number, victory: boolean, tiles: Tile[]) {
    this.score = score;
    this.size = size;
    this.victory = victory;
    this.tiles = tiles;
  }
}
