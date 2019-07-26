import { Router } from '@angular/router';
import { Injectable, Output, EventEmitter } from '@angular/core';
import { AuthConfig, OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { log } from 'util';
import { filter } from 'rxjs/operators';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: 'http://localhost:5000',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/home',

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'angular',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile wordsmaker_api',

  // URL that user is routed after logout
  logoutUrl: '/',
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  @Output() tokenValidState = new EventEmitter<boolean>();

  constructor(private oauthService: OAuthService, private router: Router) {
    this.configureImplicitFlow();
    this.oauthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .subscribe(_ => { this.updateToken(); });
  }

  isTokenValid() {
    const jwt = sessionStorage.getItem('id_token');
    if (jwt == null) { return false; }
    else { return true; }
  }

  getValueFromIdToken(claim: string) {
    /* const jwt = sessionStorage.getItem('id_token');
    if (jwt == null) {
      return null;
    }
    const jwtData = jwt.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData); */
    const decodedJwtJsonData = sessionStorage.getItem('id_token_claims_obj');

    let value: any;
    JSON.parse(decodedJwtJsonData, function findKey(k, v) {
      if (k === claim) {
        value = v;
      }
    });
    return value;
  }

  private configureImplicitFlow() {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  loginUser() {
    this.oauthService.initImplicitFlow();
  }

  logoutUser() {
    // true - redirect user after logout
    this.oauthService.logOut(false);
    sessionStorage.clear();
    this.router.navigateByUrl('/');
  }

  updateToken() {
    log("token_received in auth service");
    this.tokenValidState.emit(true);
  }

};
