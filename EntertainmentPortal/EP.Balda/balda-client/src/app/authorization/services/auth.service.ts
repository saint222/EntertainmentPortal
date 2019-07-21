import { Player } from './../../game/models/player';
import { UserRegistration } from './../models/userRegistration';
import { UserLogin } from './../models/userLogin';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  registerUser(userRegistration: UserRegistration) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

    return this.http.post<Player>('http://localhost:5001/api/simpleregister', userRegistration, httpOptions);
  }

  constructor(private http: HttpClient) {}

  login(userLogin: UserLogin) {
     const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

     return this.http.post<Player>('http://localhost:5001/api/simplelogin', userLogin, httpOptions);
  }

  facebookSignIn() {

  }

  googleSignIn() {

  }
}
