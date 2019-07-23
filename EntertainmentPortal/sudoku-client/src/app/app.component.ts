import { AuthService } from './typescript-angular-client/api/auth.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'sudoku-client';
  userName: string;

  constructor(private authService: AuthService) {
    this.userName = this.authService.getValueFromIdToken('name');
    this.authService.tokenValidState.subscribe(e => {
      this.updateComponent();
    });
  }

  updateComponent() {
    if (this.authService.isTokenValid()) {
      this.userName = this.authService.getValueFromIdToken('name');
    }
  }

  login() {
    this.authService.loginUser();
  }

  logout() {
    this.authService.logoutUser();
  }

}
