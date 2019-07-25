import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class LogoutService implements CanActivate {
  constructor(private accountService: AccountService) {}

  canActivate(): boolean {

    this.accountService.startLogout();
    return false;
  }
}
