import { GameService } from './../../services/game.service';
import { Component, OnInit } from '@angular/core';
import { log } from 'util';
import { AuthService } from '../../services/auth.service';
import { PlayerService } from '../../services/player.service';
import { Game } from '../../models/game';
import { Player } from '../../models/player';
import { Word } from '../../models/Word';
interface Dictionary {
  [index: number]: string;
}

@Component({
  selector: 'app-playing-field',
  templateUrl: './playing-field.component.html',
  styleUrls: ['./playing-field.component.scss']
})

export class PlayingFieldComponent implements OnInit {

  keyWord: string = '';
  keyWordLetters: string[];
  CuttedLetters: Dictionary;
  resultWord: string[];
  game: Game = null;
  player: Player = null;
  id: string = '';
  word: Word = null;

  wordsArray: Word[] = [];
  public CLearBtnDisable: boolean;

  constructor(private playerService: PlayerService, private gameService: GameService, private authService: AuthService) {
    // this.keyWord = gameService.getGame(playerService.);
    this.CuttedLetters = {};
    this.resultWord = [];
  }

  ngOnInit() {
    this.loadPlayer();

  }

  loadPlayer()
  {
    if(this.authService.isTokenValid())
    {
      var temp = this.authService.getValueFromIdToken('sub');
      this.playerService.getPlayer(temp).subscribe(p =>
        {
          this.player = p;
          if(this.player !== null)
          {
            this.loadGame(this.player.id);
          }
        });
    }
  }
  loadGame(playerId: string)
  {
    if(this.authService.isTokenValid())
    {
      this.gameService.getGame(playerId).subscribe(p =>
        {
          this.game = p;
          this.keyWord = this.game.keyWord;
          this.keyWordLetters = this.keyWord.split('');
          this.gameService.getAllWords(this.game.id).subscribe(p =>{
            this.wordsArray = p;
          });
        }, err => {
          this.createGame(playerId);
        });
    }
  }
  createGame(playerId: string)
  {
    this.gameService.createGame(playerId).subscribe(p =>
      {
        this.game = p;
        this.keyWord = this.game.keyWord;
        this.keyWordLetters = this.keyWord.split('');
      });
  }

  CellClick(index: number) {
    log(index.toString());
    if (this.keyWordLetters[index] === '_') {
      //this.keyWordLetters[index] = this.CuttedLetters[index];
      //this.CuttedLetters[index] = null;
      //this.resultWord.pop();
    }
    else
    {

      this.CuttedLetters[index] = this.keyWordLetters[index];
      this.resultWord.push(this.keyWordLetters[index]);
      this.keyWordLetters[index] = '_';
    }

  }
  ClearBtnClick()
  {
    this.keyWordLetters = this.keyWord.split('');
    this.resultWord.length = 0;
    this.CuttedLetters = {};
  }
  SubmitBtnClick() {

    this.gameService.submitWord(this.resultWord.join(''), this.game.id).subscribe(p =>
      {
        this.wordsArray.push(p);
        this.wordsArray.forEach(element => {
          log("Returned word - " + element.value+", ")
        });
        this.ClearBtnClick();
      })
  }

  CheckResultWord() {
    return this.resultWord.length > 0 ? false : true;
  }

}
