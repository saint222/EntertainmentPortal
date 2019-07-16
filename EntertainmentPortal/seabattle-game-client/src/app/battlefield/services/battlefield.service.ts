import { element } from 'protractor';
import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { Observable } from 'rxjs';
import { Ship } from 'src/app/models/ship';
import { CellStatus } from 'src/app/models/cellStatus';

@Injectable({
  providedIn: 'root'
})
export class BattlefieldService {

  shipField: Cell[][];
  ships: Ship[];
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
   }

  addShip(form: FormGroup) {
    const rank = form.value.rank;
    this.http.post<Ship[]>('http://localhost:54708/api/Ships/add', form.value).subscribe(data => {
      this.ships = data;
      this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
      this.calculateShipCountByRank(); // тут считаем количество кораблей
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
      this.shipField[cell.x][cell.y].status = cell.status;
    });
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
