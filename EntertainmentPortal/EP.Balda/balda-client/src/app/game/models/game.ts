import { Playground } from './playground';

export class Game {
    id: number;
    mapId: number;
    map: Playground;
    isPlayersTurn: boolean;
    playerScore: number;
    opponentScore: number;
    isGameOver: boolean;
}
