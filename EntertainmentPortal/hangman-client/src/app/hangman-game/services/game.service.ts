import { environment } from './../../../environments/environment';
import { GameData } from './../models/game-data';
import { Injectable, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserData } from '../models/user-data';
import { Router } from '@angular/router';
import { OAuthService, AuthConfig, JwksValidationHandler } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: 'http://localhost:5000',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/startScreen',

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'spa',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile hangman_api',
};

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient, private router: Router, private authService: OAuthService) {
    this.authService.configure(authConfig);
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocumentAndTryLogin();
  }

  private url = `${environment.base_url}/api/PlayHangman`;

  // Gets auth server's token from session storage
  getAccessToken() {
    return sessionStorage.getItem('access_token');
  }

  makeAccessTokenString() {
    return `Bearer ${this.getAccessToken()}`;
  }

  checkAccessTokenExistance() {
    if (this.getAccessToken() == null) {
      alert('You are not logged in. Application is available only for registered users, so please, log in or register');

      this.router.navigateByUrl('/');
    }
  }


  createGame() {
    this.checkAccessTokenExistance();
    return this.http.post<GameData>(this.url, null, {headers: {Authorization: this.makeAccessTokenString()}});
  }

  updateGame(responseModel: GameData) {
    this.checkAccessTokenExistance();
    return this.http.put<GameData>(this.url, responseModel, {headers: {Authorization: this.makeAccessTokenString()}});
  }

  deleteGame(id: number) {
    this.checkAccessTokenExistance();
    return this.http.delete(this.url + `/${id}`, {headers: {Authorization: this.makeAccessTokenString()}});
  }

  loginUser() {
    return this.authService.initImplicitFlow();
  }
}
