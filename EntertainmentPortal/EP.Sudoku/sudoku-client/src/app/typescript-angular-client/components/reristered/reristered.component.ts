import { Router } from '@angular/router';
import { AuthConfig, OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { Component, OnInit } from '@angular/core';

// export const authConfig: AuthConfig = {

//   // Url of the Identity Provider
//   issuer: 'https://localhost:44366',

//   // URL of the SPA to redirect the user to after login
//   redirectUri: window.location.origin + '/home',

//   // The SPA's id. The SPA is registerd with this id at the auth-server
//   clientId: 'angular',

//   // set the scope for the permissions the client should request
//   // The first three are defined by OIDC. The 4th is a usecase-specific one
//   scope: 'openid profile sudoku_api',
// }

@Component({
  selector: 'app-reristered',
  templateUrl: './reristered.component.html',
  styleUrls: ['./reristered.component.scss']
})
export class ReristeredComponent implements OnInit {

  // userName: string = this.getValueFromIdToken('name');

  constructor(private authService: OAuthService, private router: Router) {
    // this.authService.configure(authConfig);
    // this.authService.tokenValidationHandler = new JwksValidationHandler();
    // this.authService.loadDiscoveryDocumentAndTryLogin();
  }

  ngOnInit() {
  }

  // login() {
  //   this.authService.initImplicitFlow();
  // }

  // logout() {
  //   this.authService.logOut();
  //   this.router.navigateByUrl('/');

  // }

  // getValueFromIdToken(claim: string) {
  //   const jwt = sessionStorage.getItem('id_token');
  //   if ( jwt == null) {
  //     return null;
  //   }

  //   const jwtData = jwt.split('.')[1];
  //   const decodedJwtJsonData = window.atob(jwtData);
  //   let value: any;
  //   JSON.parse(decodedJwtJsonData, function findKey(k, v) {
  //     if (k === claim) {
  //       value = v;
  //     }
  //   });
  //   console.log(value);
  //   return value;
  // }
}
