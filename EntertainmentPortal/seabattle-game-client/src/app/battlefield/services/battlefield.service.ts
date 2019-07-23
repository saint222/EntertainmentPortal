import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { Ship } from 'src/app/models/ship';
import { CellStatus } from 'src/app/models/cellStatus';

@Injectable({
  providedIn: 'root'
})
export class BattlefieldService {

  shipField: Cell[][];
  ships: Ship[];
  shots: Cell[];
  oneLast: number;
  twoLast: number;
  threeLast: number;
  fourLast: number;
  N = 10;
  constructor(private http: HttpClient) {
    this.shipField = this.createField();
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
    this.ships = new Array<Ship>();
    this.shots = new Array<Cell>();
   }

  addShip(form: FormGroup) {
    const rank = form.value.rank;
    this.http.post<Ship[]>('http://localhost:54708/api/Ships/add', form.value).subscribe(data => {
      if (data != null && data !== undefined) {
        this.ships = data;
        this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
        this.calculateShipCountByRank(); // тут считаем количество кораблей
      }
    });
    // ------------gogno-------------
    let json = { gameId: '1', answeredPlayerId: '1'};
    this.http.get<Cell[]>('http://localhost:54708/api/Shot/get', { params: json }). subscribe(data => {
      if (data != null && data !== undefined) {
        this.shots = data;
        this.shots.forEach(shot => this.drawShot(shot));
      }
    });
  }

  private createField(): Cell[][] {
    let field = [];
    for (let i = 0; i < this.N; i++) {
      field[i] = [];
      for (let j = 0; j < this.N; j++) {
          field[i][j] = new Cell(i, j, CellStatus.None);
      }
    }
    return field;
  }

  getShipField(): Cell[][] {
    return this.shipField;
  }

  private drawShip(ship: Ship) {
    ship.cells.forEach(cell => {
      this.shipField[cell.y][cell.x].status = cell.status;
    });
  }

  private drawShot(shot: Cell) {
    this.shipField[shot.y][shot.x].status = shot.status;
  }

  private calculateShipCountByRank() {
    let one = 0;
    let two = 0;
    let three = 0;
    let four = 0;
    this.ships.forEach(ship => {
      switch (ship.rank) {
        case 1:
          one += 1;
          break;
        case 2:
          two += 1;
          break;
        case 3:
          three += 1;
          break;
        case 4:
          four += 1;
          break;
      }
    });
    this.oneLast = 4 - one;
    this.twoLast = 3 - two;
    this.threeLast = 2 - three;
    this.fourLast = 1 - four;
  }
}
