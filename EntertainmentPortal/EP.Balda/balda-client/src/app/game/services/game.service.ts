import { Map } from './../models/map';
import { Game } from './../models/game';
import { BehaviorSubject } from 'rxjs';
import { CreateNewGame } from './../models/createNewGame';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { runInThisContext } from 'vm';
import { Cell } from '../models/cell';

@Injectable({
  providedIn: 'root'
})

export class GameService {

  private gameSource = new BehaviorSubject<Game>(new Game());
  currentGame = this.gameSource.asObservable();

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

  getGame() {
    return this.http.get('http://localhost:5001/api/game');
  }

  changeGameSource(game: Game) {
    this.gameSource.next(game);
  }
}
