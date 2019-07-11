import { AccountService } from './account.service';
import { CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthguardService implements CanActivate {
  constructor(private accountService: AccountService) {}

  canActivate(): boolean {
    if (this.accountService.IsLoggedIn()) {
        return true;
    }

    this.accountService.startAuthentication();
    return false;
  }
}
