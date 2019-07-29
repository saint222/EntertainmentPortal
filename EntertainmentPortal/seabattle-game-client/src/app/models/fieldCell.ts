import { CellStatus } from './cellStatus';

export class fieldCell {
  x: number;
  y: number;
  status: CellStatus;

  constructor(x: number, y: number, status: CellStatus) {
    this.x = x;
    this.y = y;
    this.status = status;
  }

}