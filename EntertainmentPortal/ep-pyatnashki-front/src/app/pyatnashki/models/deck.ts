import { Tile } from './tile';
export class Deck {
  size: number;
  score: number;
  victory: boolean;
  tiles: Tile[][];

  constructor(score: number, size: number, victory: boolean, tiles: Tile[]) {
    this.score = score;
    this.size = size;
    this.victory = victory;
    this.tiles = new Array<Tile[]>(size);
    for (let i = 0; i < size; i++) {
        this.tiles[i] = tiles.slice(size * i, size * (i + 1));
    }

  }
}
