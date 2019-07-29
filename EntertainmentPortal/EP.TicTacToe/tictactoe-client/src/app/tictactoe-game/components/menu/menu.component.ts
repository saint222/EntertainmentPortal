import {log} from 'util';
import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';
import {AuthService} from 'src/app/core/services/auth.service';
import {Router} from '@angular/router';
import {MatDialog} from '@angular/material';
import {AboutDialogComponent} from '../about-dialog/about-dialog.component';
import {CallDialogComponent} from '../call-dialog/call-dialog.component';
import {GameSetupComponent} from '../game-setup/game-setup.component';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent implements OnInit {

  userName: string;
  userEmail: string;
  userId: string;
  login: boolean;
  private mapSize: number;
  isDisable = false;

  constructor(
    private authService: AuthService,
    public router: Router,
    private share: ShareService,
    public dialogAbout: MatDialog,
    public dialogCall: MatDialog,
    public dialogSetup: MatDialog,
  ) {
    this.authService.tokenValidState.subscribe(e => {
      log('Token event received in HOME Component');
      this.updateComponent();
    });
  }

  ngOnInit() {
    this.share.currentMessage.subscribe(message => this.mapSize = message);
    this.updateComponent();
  }

  newSender() {
    this.share.changeMapSize(this.mapSize);
  }

  homeBtnClick() {
    const navigate = this.router.navigate(['/menu']);
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
      this.userId = this.authService.getValueFromIdToken('id');
      this.userName = this.authService.getValueFromIdToken('name');
      this.userEmail = this.authService.getValueFromIdToken('email');
    }
  }

  openAboutDialog(): void {
    const dialogRef = this.dialogAbout.open(AboutDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      console.log('The about dialog was closed');
    });
  }

  openCallDialog() {
    const dialogRef = this.dialogCall.open(CallDialogComponent, {});

    dialogRef.afterClosed().subscribe(result => {
      console.log('The call dialog was closed');
    });
  }

  setupBtnClick() {
    const dialogRef = this.dialogSetup.open(GameSetupComponent, {
      data: {
        currentUserName: this.userName
      }
    });

    this.isDisable = true;

    dialogRef.afterClosed().subscribe(result => {
      console.log('The setup dialog was closed');
      console.log(result);
    });
  }

  gameBtnClick() {
    this.isDisable = false;
    const navigate = this.router.navigate(['/board']);
  }

}
