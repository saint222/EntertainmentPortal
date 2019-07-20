import { Player } from './../models/player';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(private http: HttpClient) { }



  getPlayers() {
    return this.http.get<Player[]>('http://localhost:44350/api/player');
  }

  getPlayer(id: number) {
    const params = new HttpParams().set('number', id.toString());
    return this.http.get('http://localhost:44350/api/player', {params});
  }

  addPlayer(player: Player) {
      return this.http.post('http://localhost:44350/api/player', player);
  }

}
