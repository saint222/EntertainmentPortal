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
  N = 10;
  constructor(private http: HttpClient) {
    this.shipField = this.createField();
   }

  addShip(form: FormGroup): Cell[][] {
    const ships = this.http.post<Ship[]>('http://localhost:54708/api/Ships/add', form.value);
    ships.subscribe(val => val.forEach(ship => this.drawShip(ship)));
    return this.getShipField();
  }

  createField(): Cell[][] {
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

  drawShip(ship: Ship) {
    ship.cells.forEach(cell => {
      this.shipField[cell.x][cell.y].status = cell.status;
    });
  }
}
