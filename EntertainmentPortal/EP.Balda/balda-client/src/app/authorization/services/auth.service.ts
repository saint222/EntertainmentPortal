import { UserRegistration } from './../models/userRegistration';
import { UserLogin } from './../models/userLogin';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  registerUser(userRegistration: UserRegistration) {
    return this.http.post('http://localhost:5001/api/register', userRegistration);
  }

  constructor(private http: HttpClient) {}

  login(userLogin: UserLogin) {
    const httpOptions = {
      // headers: new HttpHeaders({
      //  Authorization: 'forms'
      // }),
      withCredentials: true
     };

    return this.http.post('http://localhost:5001/api/simplelogin', userLogin, httpOptions);
  }
}
