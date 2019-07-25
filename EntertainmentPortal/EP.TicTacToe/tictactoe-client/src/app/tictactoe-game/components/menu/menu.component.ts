import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../../core/services/auth.service';
import {Router} from '@angular/router';
import {log} from 'util';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  userName: string;
  userEmail: string;
  // userId: string;
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
    // this.userId = null;
    this.userEmail = null;
  }

  updateComponent() {
    if (this.authService.isTokenValid()) {
      this.login = true;
      this.userName = this.authService.getValueFromIdToken('name');
      // this.userName = this.authService.getValueFromIdToken('id');
      this.userEmail = this.authService.getValueFromIdToken('email');
    }
  }

}
