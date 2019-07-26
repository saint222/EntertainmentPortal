export interface IGameMap {
  id: number;
  mapSize: number;
  gameId: number;
}

export class GameMap implements IGameMap {
  id: number;
  mapSize: number;
  gameId: number;

  constructor(getMap: any) {
    this.id = getMap.id;
    this.mapSize = getMap.mapSize;
    this.gameId = getMap.gameId;
  }
}
