import { Deck } from './../models/deck';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DeckService {

  private url = 'https://localhost:44380/api/deck';
  constructor(private http: HttpClient) { }

  newDeck() {
    return this.http.post<Deck>(this.url, null);
  }

  moveTile(num: number) {
    return this.http.put<Deck>(this.url, num);
  }

  getDeck() {
    return this.http.get<Deck>(this.url);
  }


}
