import {log} from 'util';
import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';
import {AuthService} from 'src/app/core/services/auth.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {

  stringMessage: string;
  userName: string;
  userEmail: string;
  userId: string;
  login: boolean;
  private mapSize: number;

  constructor(
    private authService: AuthService,
    public router: Router,
    private share: ShareService
  ) {
    this.authService.tokenValidState.subscribe(e => {
      log('Token event received in HOME Component');
      this.updateComponent();
    });
  }

  ngOnInit() {
    this.share.onMapClick.subscribe(cnt => this.mapSize = cnt);
    // this.share.currentMessage.subscribe(message => this.stringMessage = message);
    this.share.currentMessage.subscribe(message => this.mapSize = message);
    this.updateComponent();
  }

  newSender() {
    // this.share.changeMessage('Hello from Sibling');
    this.share.changeMessage(this.mapSize);
  }

  homeBtnClick() {
    const navigate = this.router.navigate(['/menu']);
  }

  ord_3_BtnClick() {
    this.mapSize = 10;
    // this.share.doClick(this.mapSize);
    this.newSender();
    const navigate = this.router.navigate(['/board']);
  }

  loginBtnClick() {
    this.login = true;
    log('Pressed btn to login user');
    this.authService.loginUser();
  }

  logoutBtnClick() {
    this.login = false;

    log('Pressed btn to logout user');
    this.authService.logoutUser();
    this.userName = null;
    this.userEmail = null;
    this.userId = null;
  }

  updateComponent() {
    if (this.authService.isTokenValid()) {
      this.login = true;
      this.userName = this.authService.getValueFromIdToken('name');
      this.userEmail = this.authService.getValueFromIdToken('email');
      this.userId = this.authService.getValueFromIdToken('id');
    }
  }
}
