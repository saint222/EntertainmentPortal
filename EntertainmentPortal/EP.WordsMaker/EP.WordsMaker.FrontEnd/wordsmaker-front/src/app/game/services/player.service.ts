import { Player } from './../models/player';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  getAccessToken() {
    return sessionStorage.getItem('access_token');
  }

  makeAccessTokenString() {
    return `Bearer ${this.getAccessToken()}`;
  }

  constructor(private http: HttpClient) { }

  getPlayers() {
    console.log("GetPlayersExecute");
    return this.http.get<Player[]>('https://localhost:44350/api/player');
  }

  getPlayer(id: string) {
    //const params = new HttpParams().set('id', id);
    console.log("GetPlayerExecute");
    return this.http.get<Player>('https://localhost:44350/api/player'+ `/${id}`);
  }

  addPlayer(name: string, id: string, email: string) {
    console.log("AddPlayersExecute");
    return this.http.post<Player>('https://localhost:44350/api/player', {name, id, email});
  }

}
