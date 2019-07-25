import {log} from 'util';
import {Router} from '@angular/router';
import {Component, OnInit} from '@angular/core';

import {AuthService} from '../../../core/services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userName: string;
  userEmail: string;
  userId: string;
  login = false;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.tokenValidState.subscribe(e => {
      log('Token event received in HOME Component');
      this.updateComponent();
    });
  }

  ngOnInit() {
    this.updateComponent();
  }

  loginBtnClick() {
    log('Pressed btn to login user');
    this.authService.loginUser();
  }

  logoutBtnClick() {
    log('Pressed btn to logout user');
    this.authService.logoutUser();
    this.userName = null;
    this.userId = null;
    this.userEmail = null;
  }

  updateComponent() {
    if (this.authService.isTokenValid()) {
      this.login = true;
      this.userName = this.authService.getValueFromIdToken('name');
      this.userName = this.authService.getValueFromIdToken('id');
      this.userEmail = this.authService.getValueFromIdToken('email');
    }
  }
}
