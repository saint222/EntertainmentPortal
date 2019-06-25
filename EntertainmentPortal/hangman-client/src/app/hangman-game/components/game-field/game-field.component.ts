import { GameData } from './../../models/game-data';
import { GameService } from './../../services/game.service';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-game-field',
  templateUrl: './game-field.component.html',
  styleUrls: ['./game-field.component.sass']
})
export class GameFieldComponent implements OnInit {
  gameFieldData: GameData = null;
  tmpGameData: GameData = null;

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  checkLetter(letter: string) {
    this.tmpGameData = new GameData();

    this.tmpGameData.id = this.gameFieldData.id;
    this.tmpGameData.letter = letter;
    this.tmpGameData.alphabet = null;
    this.tmpGameData.correctLetters = null;
    this.tmpGameData.userErrors = 0;

    this.gameService.updateGame(this.tmpGameData).subscribe(b => {
      this.gameFieldData = b;
    },
        (err: HttpResponseBase) => console.log(err.statusText)
    );
  }

  startNewGame() {
    this.gameService.createGame().subscribe(b => {
      this.gameFieldData = b;
    },
      (err: HttpResponseBase) => console.log(err.statusText));
  }
}
