import { Player } from './../models/player';
import { GameAndCells } from './../models/gameAndCells';
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
  private userSource = new BehaviorSubject<Player>(new Player());
  currentUser = this.userSource.asObservable();

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

  changeUserSource(user: Player) {
    this.userSource.next(user);
  }

  getMap(mapid: string) {
   const myParams = new HttpParams().set('id', mapid);
   const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
   return this.http.get<Cell[][]>('http://localhost:5001/api/map', { headers: myHeaders, params: myParams, withCredentials: true });
  }

  sendWord(gameAndCells: GameAndCells) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

    return this.http.put<Cell[][]>('http://localhost:5001/api/map/word', gameAndCells, httpOptions);
  }

  getPlayer(id: string) {
    const myParams = new HttpParams().set('id', id);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<Player>('http://localhost:5001/api/player', { headers: myHeaders, params: myParams, withCredentials: true });
  }

  getCurrentPlayer() {
    return this.http.get<string>('http://localhost:5001/api/currentplayer', { withCredentials: true });
  }

  getAlphabet() {
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<string[]>('http://localhost:5001/api/map/alphabet', { headers: myHeaders, withCredentials: true });
  }

  getPlayersWords(gameId: string) {
    const myParams = new HttpParams().set('gameId', gameId);
    const myHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.get<string[]>('http://localhost:5001/api/player/words',
    { headers: myHeaders, params: myParams, withCredentials: true });
  }
}
