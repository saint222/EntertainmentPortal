import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { CellStatus } from 'src/app/models/cellStatus';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ShootService {

  shots: Cell[]; // коллекция, которая передается в сервис
  shotField: Cell[][];
  enemyShots: Subject<Cell[]>;
  endgameMessage: string;
  N = 10;
  constructor(private http: HttpClient) {
    this.shotField = this.createField();
    this.shots = new Array<Cell>();
    this.enemyShots = new Subject();
   }

  addShot(x: number, y: number, gameId: string, playerId: string) {
    const json = { x, y, gameId, playerId };
    this.http.post<Cell[]>(`${environment.base_url}api/Shot/add`, json, {withCredentials: true}).subscribe(data => {
        this.shots = data;
        this.shots.forEach(shot => this.drawShot(shot)); // тут рисуем выстрелы
        if (this.shots.filter(shot => shot.status === CellStatus.Destroyed).length >= 20) {
          this.endgameMessage = 'ВЫ ПОБЕДИЛИ';
        }
        const jsonGet = { gameId: '1', answeredPlayerId: '1'};
        this.http.get<Cell[]>(`${environment.base_url}api/Shot/get`, { params: jsonGet, withCredentials: true }). subscribe(dataGet => {
           if (dataGet != null && dataGet !== undefined) {
             this.enemyShots.next(dataGet);
           }
           if (dataGet.filter(shot => shot.status === CellStatus.Destroyed).length >= 20) {
             this.endgameMessage = 'ВЫ ПРОИГРАЛИ';
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

  reset() {
    this.shotField = this.createField();
    this.shots = new Array<Cell>();
    this.endgameMessage = '';
  }
}
