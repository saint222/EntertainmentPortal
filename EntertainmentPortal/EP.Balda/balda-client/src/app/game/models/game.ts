import { Map } from './map';
import { Player } from './player';

export class Game {
    id: number;
    initWord: string;
    mapId: number;
    map: Map;
    playerIdTurn: string;
    players: Player[];
}
