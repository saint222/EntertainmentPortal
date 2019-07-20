export class Cell {
  constructor(public id: number, public mapId: number, public x: number, public y: number, public letter: string) {
    this.id = id;
    this.mapId = mapId;
    this.x = x;
    this.y = y;
    this.letter = letter;
  }
}
