import { Cell } from './cell';

export class Ship {
    id: string;
    rank: number;
    cells: Cell[];
    isAlive: boolean;
}
