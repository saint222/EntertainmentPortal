import { GameData } from './../models/game-data';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { }

  createGame() {
    return this.http.post<GameData>('http://localhost:5000/api/PlayHangman', null);
  }
}
