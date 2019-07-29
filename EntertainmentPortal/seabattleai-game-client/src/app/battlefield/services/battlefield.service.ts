import { environment } from './../../../environments/environment.prod';
import { ShootService } from './shoot.service';
import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { Ship } from 'src/app/models/ship';
import { CellStatus } from 'src/app/models/cellStatus';
import { Subscription } from 'rxjs/internal/Subscription';

@Injectable({
  providedIn: 'root'
})
export class BattlefieldService {

  shipField: Cell[][];
  ships: Ship[];
  subscription: Subscription;
  oneLast: number;
  twoLast: number;
  threeLast: number;
  fourLast: number;
  N = 10;
  allowDelete: boolean;
  constructor(private http: HttpClient, public shotService: ShootService) {
    this.shipField = this.createField();
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
    this.ships = new Array<Ship>();
    this.subscription = shotService.enemyShots.subscribe(s => s.forEach(shot => this.drawShot(shot)));
    this.allowDelete = false;
   }

  addShip(form: FormGroup) {
    const rank = form.value.rank;
    this.http.post<Ship[]>(`${environment.base_url}api/Ships/add`, form.value, {withCredentials: true}).subscribe(data => {
      if (data != null && data !== undefined) {
        this.ships = data;
        this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
        this.calculateShipCountByRank(); // тут считаем количество кораблей
        if (data.length < 10 && data.length > 0) {
          this.allowDelete = true;
        } else {
          this.allowDelete = false;
        }
      }
    });
  }

  deleteShip(x: number, y: number) {
    let json = { x: x.toString(),  y: y.toString() };
    this.http.delete<Ship[]>(`${environment.base_url}api/Ships/delete`, { params: json, withCredentials: true}).subscribe(data => {
      this.shipField = this.createField();
      this.ships = data;
      data.forEach(ship => this.drawShip(ship));
      this.calculateShipCountByRank();
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

  reset() {
    this.shipField = this.createField();
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
    this.ships = new Array<Ship>();
    this.http.get<any>(`${environment.base_url}api/game/new`, {withCredentials: true}).subscribe();
    this.allowDelete = false;
  }
}
