import { UserInfo } from './../models/userinfo';
import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private config: UserManagerSettings = {
    authority: `${environment.is_url}`,
    client_id: 'spa',
    redirect_uri: `${environment.front_url}/auth-callback`,
    response_type: 'id_token token',
    scope: 'openid email profile pyatnashki_api',
  };
  private mgr = new UserManager(this.config);
  private user: User = null;

  constructor() {
      this.mgr.getUser().then(user => {
          this.user = user;
      });
  }




  startLogout() {

    sessionStorage.removeItem('deck');
    this.mgr.signoutRedirect();

  }
  completeLogout(): Promise<void> {
    return this.mgr.signoutRedirectCallback().then(user => {
        this.user = null;
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
    return new UserInfo(this.user.profile.preferred_username);
  }
}
