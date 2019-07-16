import { UserInfo } from './../models/userinfo';
import { Jwt } from './../models/jwt';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private url = 'https://localhost:44380/api/auth/';

  private config = {
    authority: 'http://localhost:5000',
    client_id: 'spa',
    redirect_uri: 'http://localhost:4200/auth-callback',
    response_type: 'id_token token',
    scope: 'openid email profile pyatnashki_api',
    post_logout_redirect_uri : 'http://localhost:4200/login',
  };
  private mgr = new UserManager(this.config);
  private user: User = null;

  constructor() {
      this.mgr.getUser().then(user => {
          this.user = user;
      });
  }






  IsLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }
  getClaims(): any {
    return this.user.profile;
  }
  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }
  getEmailnHeaderValue(): string {
    return `${this.user.profile.email}`;
  }
  startAuthentication(): Promise<void> {
    return this.mgr.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.mgr.signinRedirectCallback().then(user => {
        this.user = user;
    });
  }

  getUserInfo(): UserInfo {
    let userInfo = new UserInfo(this.user.profile.name);
    return userInfo;
  }
}
