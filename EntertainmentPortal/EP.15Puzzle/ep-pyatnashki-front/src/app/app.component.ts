import { NotifyHubService } from './pyatnashki/services/notify-hub.service';
import { AccountService } from './account/services/account.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'pyatnashki';

  constructor(private accountService: AccountService, public notifyHubService: NotifyHubService) {}
  public IsLoggedIn() {
    return this.accountService.IsLoggedIn();
  }
  public User() {
    return this.accountService.getUserInfo().userName;
  }
}
