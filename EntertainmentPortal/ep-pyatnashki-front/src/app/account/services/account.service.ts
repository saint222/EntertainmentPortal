import { UserInfo } from './../models/userinfo';
import { Jwt } from './../models/jwt';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private router: Router;
  private isLoggedIn: boolean;
  private url = 'https://localhost:44380/api/auth/';
  constructor(private http: HttpClient, router: Router) { this.router = router; }

  IsLoggedIn() {
    return this.isLoggedIn;
  }

  loginGoogle() {
    return this.http.get<Jwt>(this.url + 'google', {withCredentials: true});
  }
  loginFacebook() {
    return this.http.get<string>(this.url + 'facebook', {withCredentials: true});
  }
  loginBearer(userInfo: UserInfo) {
    return this.http.post<Jwt>(this.url + 'login', userInfo, {withCredentials: true});
  }


}
