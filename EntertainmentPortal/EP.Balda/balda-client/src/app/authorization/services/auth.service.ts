import { UserLogin } from './../models/userLogin';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {}

  login(userLogin: UserLogin) {
    return this.http.post('http://localhost:5001/api/simplelogin', userLogin);
  }
}
