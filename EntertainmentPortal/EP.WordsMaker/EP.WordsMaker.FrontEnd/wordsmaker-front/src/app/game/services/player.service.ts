import { Player } from './../models/player';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }



  getPlayers() {
    return this.http.get<Player[]>('http://localhost:56657/api/player');
  }

  getPlayer(id: number) {
    return this.http.get<Player>('http://localhost:56657/api/player?number' + id);
  }

  addPlayer(player: Player): Observable<Player> {

      return this.http.post<Player>('http://localhost:56657/api/player', player);
  }

}
