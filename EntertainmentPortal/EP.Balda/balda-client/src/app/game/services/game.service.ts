import { CreateNewGame } from './../models/createNewGame';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class GameService {

  constructor(private http: HttpClient) { }

  createNewGame() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    const params = new CreateNewGame('asddsa', 3);
    return this.http.post<CreateNewGame>('http://localhost:5001/api/game', params, httpOptions);
  }

  getGame() {
    return this.http.get('http://localhost:5001/api/game');
  }
}
