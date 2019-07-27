import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  waitForTokenInMsec: 10000,
  // Url of the Identity Provider
  issuer: 'https://seabattle.me:44360',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/app',

  // The SPA's id. The SPA is registered with this id at the auth-server
  clientId: 'angular',
  

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile sea-battle-2019',
  postLogoutRedirectUri: window.location.origin + '/app'
}


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'seabattle-game-client';

  constructor(private authService: OAuthService) {
    this.authService.configure(authConfig);
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocumentAndTryLogin();
    //this.authService.client
  }

   Login(){
    this.authService.initImplicitFlow();
  }
  
}
