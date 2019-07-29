import { Player } from './../../game/models/player';
import { UserRegistration } from './../models/userRegistration';
import { UserLogin } from './../models/userLogin';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


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

    return this.http.post<Player>(`${environment.base_url}api/simpleregister`, userRegistration, httpOptions);
  }

  constructor(private http: HttpClient) {}

  login(userLogin: UserLogin) {
     const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true
     };

     return this.http.post<Player>(`${environment.base_url}api/simplelogin`, userLogin, httpOptions);
  }

  facebookSignIn() {

  }

  googleSignIn() {
        const httpOptions = {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': 'http://localhost:4200/'
          }),
          withCredentials: true
         };
        return this.http.get(`${environment.base_url}api/google`, httpOptions);
  }

  logOut() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      withCredentials: true
     };

    return this.http.post(`${environment.base_url}api/logout`, httpOptions);
  }
}
