import { log } from 'util';
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

  loginBtnMsg: string;
  userName: string = this.getValueFromIdToken('name');
  userEmail: string = this.getValueFromIdToken('email');

  loginStatus = false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    if (this.userName) {
      this.loginStatus = true;
      log('User login');
    }
    else {
      this.loginStatus = false;
      log('User not login');
    }
  }
  loginBtnClick() {
    log('Pressed btn to login user');
    this.loginStatus = true;
    this.authService.loginUser();
  }
  logoutBtnClick() {
    log('Pressed btn to logout user');
    this.loginStatus = false;
    this.authService.logoutUser();
    this.userName = null;
    this.userEmail = null;
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
