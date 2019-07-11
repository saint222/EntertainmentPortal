import { AccountService } from 'src/app/account/services/account.service';
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
  constructor(private http: HttpClient, private accountService: AccountService) {
   }




  newDeck() {
  // tslint:disable-next-line: max-line-length
    return this.http.post<Deck>(this.url, null, {headers: {Authorization: this.accountService.getAuthorizationHeaderValue()} , withCredentials: true});
  }

  moveTile(num: number) {
// tslint:disable-next-line: max-line-length
    return this.http.put<Deck>(this.url, num, {headers: {Authorization: this.accountService.getAuthorizationHeaderValue()} , withCredentials: true});
  }

  getDeck() {
// tslint:disable-next-line: max-line-length
    return this.http.get<Deck>(this.url, {headers: {Authorization: this.accountService.getAuthorizationHeaderValue()}, withCredentials: true});
  }

}
