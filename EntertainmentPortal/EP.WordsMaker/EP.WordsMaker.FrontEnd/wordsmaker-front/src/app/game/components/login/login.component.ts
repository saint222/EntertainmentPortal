import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { HttpResponseBase } from '@angular/common/http';
import { GameService } from './../../services/game.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  userName: string = this.getValueFromIdToken('name');
  userEmail: string = this.getValueFromIdToken('email');

  constructor(private authService: AuthService, private router: Router) { }

  private loginStatus = false;
  ngOnInit() {
  }

  loginBtnClick(){
    if(!this.loginStatus)
    {
      this.loginStatus = !this.loginStatus;
      this.authService.loginUser();
    }
    else
    {
      this.loginStatus = !this.loginStatus;
      this.authService.logoutUser();
    }
  }
  getValueFromIdToken(claim: string) {
    const jwt = sessionStorage.getItem('id_token');
    if ( jwt == null) {
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


}
