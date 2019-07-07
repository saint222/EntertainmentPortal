import { User } from './../models/user';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = 'https://localhost:44380/api/user';
  constructor(private http: HttpClient) { }

  sendInfo(user: User) {
    return this.http.put<User>(this.url, user, { withCredentials: true });
  }

  getInfo() {
    return this.http.get<User>(this.url, { withCredentials: true });
  }
}
