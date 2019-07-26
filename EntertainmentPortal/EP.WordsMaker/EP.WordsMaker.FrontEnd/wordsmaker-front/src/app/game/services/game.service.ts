import { AuthService } from './auth.service';
import { environment } from './../../../environments/environment';
import { Game } from './../models/game';
import { Injectable, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Word } from '../models/Word';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) {}

  private url = `${environment.base_url}/api/Game`;

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

  createGame(id: string) {
    if (this.authService.isTokenValid()) {
      return this.http.post<Game>('https://localhost:44350/api/game', {PlayerId: id} );
    }
  }

  updateGame(responseModel: Game) {
    if (this.IsAccessTokenExists()) {
      return this.http.put<Game>(this.url, responseModel, {headers: {Authorization: this.makeAccessTokenString()}});
    }
  }

  deleteGame(id: number) {
    if (this.IsAccessTokenExists()) {
      return this.http.delete(this.url + `/${id}`, {headers: {Authorization: this.makeAccessTokenString()}});
    }
  }

  submitWord(Value: string, GameId: string) {
    if (this.authService.isTokenValid()) {
      return this.http.post<Word>('https://localhost:44350/api/word/submitWord', {value: Value , gameId: GameId});
    }
  }
  getAllWords(GameId: string)
  {
    if (this.authService.isTokenValid())
    {
      return this.http.get<Word[]>('https://localhost:44350/api/word/allwords' + `/${GameId}`);
    }
  }

  getGame(id: string)
  {
    if (this.authService.isTokenValid())
    {
      return this.http.get<Game>('https://localhost:44350/api/game' + `/${id}`);
    }
  }
}
