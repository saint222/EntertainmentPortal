import { GameData } from './../../models/game-data';
import { GameService } from './../../services/game.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-alphabet-buttons',
  templateUrl: './alphabet-buttons.component.html',
  styleUrls: ['./alphabet-buttons.component.sass']
})
export class AlphabetButtonsComponent implements OnInit {
  @Input() gameDataAlphabet: GameData;
  @Output() updatedGameData = new EventEmitter();
  temp: GameData;

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }
  click(letter: string) {
    this.gameDataAlphabet.letter = letter;
    this.gameDataAlphabet.alphabet = null;
    this.gameDataAlphabet.correctLetters = null;
    this.gameDataAlphabet.userErrors = 0;
    console.log(this.gameDataAlphabet);

    /*this.gameService.updateGame(this.gameDataAlphabet).subscribe(b => {this.updatedGameData.emit(b); console.log(b); } );*/
    this.gameService.updateGame(this.gameDataAlphabet).subscribe(b => {this.temp = b; console.log(b); } );
  }

}
