import { Player } from './player';

export class Game {
    id: number;
    initWord: string;
    mapId: number;
    playerIdTurn: string;
    players: Array<Player>;
}
