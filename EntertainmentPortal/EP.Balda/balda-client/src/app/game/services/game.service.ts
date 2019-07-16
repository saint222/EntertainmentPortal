import { MapComponent } from './../components/map/map.component';
import { Playground } from './../models/playground';
import { Game } from './../models/game';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Cell } from '../models/cell';

@Injectable({
  providedIn: 'root'
})

export class GameService {

  private gameSource = new BehaviorSubject<Game>(new Game());
  currentGame = this.gameSource.asObservable();
  private mapSource = new BehaviorSubject<Playground>(new Playground());
  currentMap = this.mapSource.asObservable();

  constructor(private http: HttpClient) { }

  createNewGame(size: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

    return this.http.post<Game>('http://localhost:5001/api/game', size, httpOptions);
  }

  getGame(gameid: string) {
    const myParams = new HttpParams().set('id', gameid);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<Game>('http://localhost:5001/api/game', { headers: myHeaders, params: myParams, withCredentials: true });
  }

  changeGameSource(game: Game) {
    this.gameSource.next(game);
  }

  changeMapSource(playground: Playground) {
    this.mapSource.next(playground);
  }

  getMap(mapid: string) {
   const myParams = new HttpParams().set('id', mapid);
   const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
   return this.http.get<Cell[][]>('http://localhost:5001/api/map', { headers: myHeaders, params: myParams, withCredentials: true });
  }
}
