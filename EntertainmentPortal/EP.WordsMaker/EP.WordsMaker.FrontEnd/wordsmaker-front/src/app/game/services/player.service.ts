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
    console.log("GetPlayersExecute");
    return this.http.get<Player[]>('https://localhost:44350/api/player');

  }

  getPlayer(id: number) {
    const params = new HttpParams().set('number', id.toString());
    console.log("GetPlayerExecute");
    return this.http.get('https://localhost:44350/api/player', {params});
  }

  addPlayer(player: Player) {
    console.log("AddPlayersExecute");
      return this.http.post('https://localhost:44350/api/player', player);
  }

}
