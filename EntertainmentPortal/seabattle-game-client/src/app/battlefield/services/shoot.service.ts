import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { CellStatus } from 'src/app/models/cellStatus';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShootService {

  shots: Cell[]; // коллекция, которая передается в сервис
  shotField: Cell[][];
  enemyShots: Subject<Cell[]>;
  N = 10;
  constructor(private http: HttpClient) {
    this.shotField = this.createField();
    this.shots = new Array<Cell>();
    this.enemyShots = new Subject();
   }

  addShot(x: number, y: number, gameId: string, playerId: string) {
    const json = { x, y, gameId, playerId };
    this.http.post<Cell[]>('http://localhost:54708/api/Shot/add', json).subscribe(data => {
        this.shots = data;
        this.shots.forEach(shot => this.drawShot(shot)); // тут рисуем выстрелы

        const jsonGet = { gameId: '1', answeredPlayerId: '1'};
        this.http.get<Cell[]>('http://localhost:54708/api/Shot/get', { params: jsonGet }). subscribe(data => {
           if (data != null && data !== undefined) {
             this.enemyShots.next(data);
           }
         });
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

  drawShot(shot: Cell) {
    this.shotField[shot.y][shot.x].status = shot.status;
  }
}
