import { GameData } from './../../models/game-data';
import { GameService } from './../../services/game.service';
import { Component, OnInit } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-game-field',
  templateUrl: './game-field.component.html',
  styleUrls: ['./game-field.component.sass']
})
export class GameFieldComponent implements OnInit {
  gameData: GameData = null;

  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.gameService.createGame().subscribe(b => {this.gameData = b; console.log(b)},
      (err: HttpResponseBase) => console.log(err.statusText));
  }

}
