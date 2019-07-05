import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AccountService } from './account/services/account.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: AccountService, private router: Router) {}

  canActivate() {

    if (!this.user.IsLoggedIn())
    {
       this.router.navigate(['/account/login']);
       return false;
    }

    return true;
  }
}
