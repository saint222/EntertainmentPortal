import { Playground } from './playground';
import { Player } from './player';

export class Game {
    id: number;
    initWord: string;
    mapId: number;
    map: Playground;
    playerIdTurn: string;
    players: Player[];
}
