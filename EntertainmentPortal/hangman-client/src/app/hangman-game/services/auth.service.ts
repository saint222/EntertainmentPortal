import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

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

  // URL that user is routed after logout
  logoutUrl: '/',
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private authService: OAuthService, private router: Router) {
    this.authService.configure(authConfig);
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocumentAndTryLogin();
   }

  loginUser() {
    return this.authService.initImplicitFlow();
  }

  logoutUser() {
    // true - redirect user after logout
    sessionStorage.clear();
    this.router.navigateByUrl('/');

  }
}
