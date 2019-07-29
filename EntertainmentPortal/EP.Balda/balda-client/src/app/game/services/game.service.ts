import { GameResults } from './../models/gameResults';
import { CurrentGame } from '../models/currentGame';
import { Player } from './../models/player';
import { GameAndCells } from './../models/gameAndCells';
import { Game } from './../models/game';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Cell } from '../models/cell';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class GameService {

  constructor(private http: HttpClient) { }

  createNewGame(size: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

    return this.http.post<CurrentGame>(`${environment.base_url}api/game`, size, httpOptions);
  }

  getGame(gameid: string) {
    const myParams = new HttpParams().set('id', gameid);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<Game>(`${environment.base_url}api/game`, { headers: myHeaders, params: myParams, withCredentials: true });
  }

  sendWord(gameAndCells: GameAndCells) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

    return this.http.put<Game>(`${environment.base_url}api/game/word`, gameAndCells, httpOptions);
  }

  getPlayer(id: string) {
    const myParams = new HttpParams().set('id', id);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<Player>(`${environment.base_url}api/player`, { headers: myHeaders, params: myParams, withCredentials: true });
  }

  getCurrentGame() {
    return this.http.get<CurrentGame>(`${environment.base_url}api/currentGame`, { withCredentials: true });
  }

  getAlphabet() {
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<string[]>(`${environment.base_url}api/map/alphabet`, { headers: myHeaders, withCredentials: true });
  }

  getPlayersWords(gameId: string) {
    const myParams = new HttpParams().set('gameId', gameId);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<string[]>(`${environment.base_url}api/player/words`,
    { headers: myHeaders, params: myParams, withCredentials: true });
  }

  getPlayersOpponentWords(gameId: string) {
    const myParams = new HttpParams().set('gameId', gameId);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<string[]>(`${environment.base_url}api/playerOpponent/words`,
    { headers: myHeaders, params: myParams, withCredentials: true });
  }

  leaveGame(gameId: string) {
    const myParams = new HttpParams().set('gameId', gameId);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/text');
    return this.http.delete<Game>(`${environment.base_url}api/leaveGame`,
    { headers: myHeaders, params: myParams, withCredentials: true });
  }

  getMap(mapId: string) {
    const myParams = new HttpParams().set('Id', mapId);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<Cell[][]>(`${environment.base_url}api/map`,
    { headers: myHeaders, params: myParams, withCredentials: true });
  }

  getResults() {
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<GameResults>(`${environment.base_url}api/game/results`, { headers: myHeaders, withCredentials: true });
  }
}
