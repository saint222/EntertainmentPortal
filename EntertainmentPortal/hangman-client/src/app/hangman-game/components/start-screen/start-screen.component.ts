import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

import { AuthConfig } from 'angular-oauth2-oidc';

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


@Component({
  selector: 'app-start-screen',
  templateUrl: './start-screen.component.html',
  styleUrls: ['./start-screen.component.sass']
})
export class StartScreenComponent implements OnInit {

  constructor(private router: Router, private authService: OAuthService) {
    this.authService.configure(authConfig);
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocumentAndTryLogin();
   }

  ngOnInit() {
  }

  startGame() {
    this.router.navigateByUrl('/gameSession');
  }

  loginUser() {
    // this.router.navigateByUrl('/login');
    this.authService.initImplicitFlow();
  }

  registerUser() {
    this.router.navigateByUrl('/register');
  }
}
