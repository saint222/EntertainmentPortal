import { environment } from './../../../environments/environment.prod';
import { AccountService } from 'src/app/account/services/account.service';
import { Deck } from './../models/deck';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DeckService {

  private url = `${environment.api_url}/api/deck`;
  constructor(private http: HttpClient, private accountService: AccountService) {
   }




  newDeck() {
  // tslint:disable-next-line: max-line-length
    return this.http.post<Deck>(this.url, this.accountService.getUserInfo(), {headers: new HttpHeaders ({
      Authorization: this.accountService.getAuthorizationHeaderValue(),
      // tslint:disable-next-line: object-literal-key-quotes
      'Email': this.accountService.getEmailnHeaderValue()
    }) , withCredentials: true});
  }

  moveTile(num: number) {
// tslint:disable-next-line: max-line-length
    return this.http.put(this.url, num, {headers: new HttpHeaders ({
      Authorization: this.accountService.getAuthorizationHeaderValue(),
      // tslint:disable-next-line: object-literal-key-quotes
      'Email': this.accountService.getEmailnHeaderValue()
    }) , withCredentials: true});
  }

  getDeck() {
    return this.http.get<Deck>(this.url, {headers: new HttpHeaders ({
      Authorization: this.accountService.getAuthorizationHeaderValue(),
      // tslint:disable-next-line: object-literal-key-quotes
      'Email': this.accountService.getEmailnHeaderValue()
  }), withCredentials: true});
  }

}
