import { environment } from './../../../environments/environment';
import { GameData } from './../models/game-data';
import { Injectable, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserData } from '../models/user-data';


@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { }

  private url = `${environment.base_url}/api/PlayHangman`;

  // Gets auth server's token from session storage
  getToken() {
    return `Bearer ${sessionStorage.getItem('access_token')}`;
  }


  createGame() {
    return this.http.post<GameData>(this.url, null, {headers: {Authorization: this.getToken()}});
  }

  updateGame(responseModel: GameData) {
    return this.http.put<GameData>(this.url, responseModel, {headers: {Authorization: this.getToken()}});

  }

  deleteGame(id: number) {
    return this.http.delete(this.url + `/${id}`, {headers: {Authorization: this.getToken()}});

  }

  registerUser(userData: UserData) {
    return this.http.post(`${environment.base_url}/api/cookiesauth/register`, userData);
  }

  loginUser(userData: UserData) {
    return this.http.post(`${environment.base_url}/api/cookiesauth/login`, userData);
  }
}
