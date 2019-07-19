import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BoardComponent } from '../components/board/board.component';
import { Step } from '../models/step';
import { Map } from '../models/map';
import { NewGame } from '../models/newgame';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  constructor(private http: HttpClient) { }
  
  postNewGame(newgame: NewGame) {
    return this.http.post('http://localhost:33224/api/game/post', newgame);
  }

  getMap() {
    return this.http.get<Map>('http://localhost:33224/api/game/new');
  }

  postStep(step: Step) {
    return this.http.post('http://localhost:33224/api/game/post', step);
  }
}
