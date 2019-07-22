import { AccountService } from './account/services/account.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'pyatnashki';

  constructor(private accountService: AccountService) {}
  public IsLoggedIn() {
    return this.accountService.IsLoggedIn();
  }
}
