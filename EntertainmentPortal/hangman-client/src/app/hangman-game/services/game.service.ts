import { GameData } from './../models/game-data';
import { Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { }

  createGame() {
    return this.http.post<GameData>('http://localhost:33224/api/PlayHangman', null);
  }
  updateGame(responseModel: GameData) {

    return this.http.put<GameData>('http://localhost:33224/api/PlayHangman', responseModel);
  }
}
