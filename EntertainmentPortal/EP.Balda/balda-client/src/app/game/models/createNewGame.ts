export class CreateNewGame {
  constructor(public playerId: string, public mapSize: number) {
    this.playerId = playerId;
    this.mapSize = mapSize;
    }
}
