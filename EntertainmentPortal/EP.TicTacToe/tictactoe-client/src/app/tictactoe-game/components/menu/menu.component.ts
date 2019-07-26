import {log} from 'util';
import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';
import {tictactoe_api_host} from '../../../core/services/api.client.generated';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {

  mapSize: number;
  userName: string;
  userEmail: string;
  userId: string;
  login: boolean;

  constructor() {

  }

  // constructor(
  //   // private authService: AuthService,
  //   // private router: Router
  // ) {
  //   // this.authService.tokenValidState.subscribe(e => {
  //   //   log('Token event received in HOME Component');
  //   //   this.updateComponent();
  //   // });
  // }

  ngOnInit() {
    this.updateComponent();
  }

  ord_3_BtnClick() {
    this.mapSize = 3;

  }

  loginBtnClick() {
    this.login = true;
    this.userName = 'Allison Hauck';
    this.userEmail = 'Allison50@google.com';
    this.userId = '1';

    log('Pressed btn to login user');
    // this.authService.loginUser();
  }

  logoutBtnClick() {
    this.login = false;

    log('Pressed btn to logout user');
    // this.authService.logoutUser();
    this.userName = null;
    this.userEmail = null;
    this.userId = null;
  }

  updateComponent() {
    // if (this.authService.isTokenValid()) {
    //   this.login = true;
    //   this.userName = this.authService.getValueFromIdToken('name');
    //   this.userEmail = this.authService.getValueFromIdToken('email');
    // }
    if (this.login) {
      this.userName = 'Allison Hauck';
      this.userEmail = 'Allison50@google.com';
      this.userId = '1';
      this.login = true;
    }

  }
}
