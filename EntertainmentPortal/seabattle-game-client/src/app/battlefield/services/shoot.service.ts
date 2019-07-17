import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { CellStatus } from 'src/app/models/cellStatus';

@Injectable({
  providedIn: 'root'
})
export class ShootService {

  shots: Cell[];
  shotField: Cell[][];
  N = 10;
  constructor(private http: HttpClient) {
    this.shotField = this.createField();
    this.shots = new Array<Cell>();
   }

  addShot(x: number, y: number, gameId: string, playerId: string) {
   var json = {
     x,
     y,
     gameId,
     playerId
   };
   this.http.post<Cell[]>('http://localhost:54708/api/Shot/add', json).subscribe(data => {
      this.shots = data;
      this.shots.forEach(shot => this.drawShot(shot)); // тут рисуем выстрелы
    });
  }

  private createField(): Cell[][] {
    let field = [];
    for (let i = 0; i < this.N; i++) {
      field[i] = [];
      for (let j = 0; j < this.N; j++) {
          field[i][j] = new Cell(j, i, CellStatus.None);
      }
    }
    return field;
  }

  drawShot(shot: Cell) {
    this.shotField[shot.x][shot.y].status = shot.status;
  }
}
