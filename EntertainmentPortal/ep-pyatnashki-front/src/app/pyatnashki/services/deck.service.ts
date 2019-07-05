import { Jwt } from './../../account/models/jwt';
import { ConfigService } from './../../shared/services/config.service';
import { Deck } from './../models/deck';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DeckService {

  private url = 'https://localhost:44380/api/deck';
  constructor(private http: HttpClient) {
   }




  newDeck() {
  // tslint:disable-next-line: max-line-length
    return this.http.post<Deck>(this.url, null, {headers: {Authorization: 'Bearer ' + this.getJwt().jwt}, withCredentials: true});
  }

  moveTile(num: number) {
    return this.http.put<Deck>(this.url, num, {headers: {Authorization: 'Bearer ' + this.getJwt().jwt}, withCredentials: true});
  }

  getDeck() {
    return this.http.get<Deck>(this.url, {headers: {Authorization: 'Bearer ' + this.getJwt().jwt}, withCredentials: true});
  }

  getJwt() {
    return JSON.parse(localStorage.getItem('jwt'));
  }
}
