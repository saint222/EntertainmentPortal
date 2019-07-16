export class Cell {
  constructor(public id: number, public mapId: number, public x: number, public y: number, public letter: string, public checked: boolean) {
    this.id = id;
    this.mapId = mapId;
    this.x = x;
    this.y = y;
    this.letter = letter;
    this.checked = checked;
  }
}
