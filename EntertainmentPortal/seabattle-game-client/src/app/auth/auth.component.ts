import { Router } from '@angular/router';
import { Component, Injectable, Output, EventEmitter, OnInit} from '@angular/core';
import { AuthConfig, OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { log } from 'util';
import { filter } from 'rxjs/operators';

export const authConfig: AuthConfig = {
  waitForTokenInMsec: 100,
  // Url of the Identity Provider
  issuer: 'https://localhost:44360',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/app',

  // The SPA's id. The SPA is registered with this id at the auth-server
  clientId: 'angular',
  

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile sea-battle-2019',
  postLogoutRedirectUri: window.location.origin + '/app'
}

export class AuthService {
 
  @Output() tokenValidState = new EventEmitter<boolean>();

  constructor(private oauthService: OAuthService, private router: Router) {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    this.oauthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .subscribe(_ => { this.updateToken(); });
  }
  subscribe( f : Function )
  {
    this.oauthService.events
    .pipe(filter(e => e.type === 'token_received'))
    .subscribe(_ => { f(); });
  }

  isTokenValid() {
    const jwt = sessionStorage.getItem('id_token');
    if (jwt == null) { return false; }
    else { return true; }
  }

  getToken()
  {
    this.oauthService.getIdToken();
  }
  getValueFromIdToken(claim: string) {
    const jwt = this.oauthService.getIdToken();
    if (jwt == null) {
      return null;
    }
    const jwtData = jwt.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    let value: any;
    JSON.parse(decodedJwtJsonData, function findKey(k, v) {
      if (k === claim) {
        value = v;
      }
    });
    return value;
  }

  loginUser() {
    this.oauthService.initImplicitFlow();
  }

  logoutUser() {
    this.oauthService.logOut();
  }

  updateToken() {
    log('token_received in auth service');
    this.tokenValidState.emit(true);
  }

  hasValidAccessToken() {
    return this.oauthService.hasValidAccessToken();
  }
}

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})

export class AuthComponent implements OnInit {
  userName: string;
  isLoginDone: boolean = false;
  constructor(private authService: AuthService) {
    if(this.authService.isTokenValid())
    {
      this.updateComponent();
    }
    this.authService.tokenValidState.subscribe(e => {
      this.updateComponent();
    })
  }

  Login(){
    this.authService.loginUser();
  }
  Logout(){
    this.authService.logoutUser();
  }
  updateComponent() {
    if(this.authService.isTokenValid())
    {
      this.userName = this.authService.getValueFromIdToken("name");
      this.isLoginDone = true;
    }
  }
  ngOnInit() {
  }

}


