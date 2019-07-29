import { CellStatus } from './cellStatus';

export class Cell {
  x: number;
  y: number;
  isAlive: boolean;

  constructor(x: number, y: number, isAlive: boolean) {
    this.x = x;
    this.y = y;
    this.isAlive = isAlive;
  }

}
