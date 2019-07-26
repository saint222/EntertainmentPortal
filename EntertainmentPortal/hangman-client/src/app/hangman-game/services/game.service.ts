import { environment } from './../../../environments/environment';
import { GameData } from './../models/game-data';
import { Injectable, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient, private router: Router) {}

  private url = `${environment.base_url}/api/PlayHangman`;

  // Gets auth server's token from session storage
  getAccessToken() {
    return sessionStorage.getItem('access_token');
  }

  makeAccessTokenString() {
    return `Bearer ${this.getAccessToken()}`;
  }

  IsAccessTokenExists() {
    if (this.getAccessToken() == null) {
      alert('You are not logged in. Application is available only for registered users, so please, log in or register');

      this.router.navigateByUrl('/');
      return false;
    }
    return true;
  }

  createGame() {
    if (this.IsAccessTokenExists()) {
      return this.http.post<GameData>(this.url, null, {headers: {Authorization: this.makeAccessTokenString()}});
    }
  }

  updateGame(responseModel: GameData) {
    if (this.IsAccessTokenExists()) {
      return this.http.put<GameData>(this.url, responseModel, {headers: {Authorization: this.makeAccessTokenString()}});
    }
  }

  deleteGame(id: number) {
    if (this.IsAccessTokenExists()) {
      return this.http.delete(this.url + `/${id}`, {headers: {Authorization: this.makeAccessTokenString()}});
    }
  }
}
