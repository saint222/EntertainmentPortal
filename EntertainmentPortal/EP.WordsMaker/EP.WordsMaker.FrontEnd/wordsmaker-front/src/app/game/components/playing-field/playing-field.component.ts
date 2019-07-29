import { Component, OnInit } from '@angular/core';
import { GameService } from './../../services/game.service';

import { log } from 'util';
import { AuthService } from '../../services/auth.service';
import { PlayerService } from '../../services/player.service';
import { Game } from '../../models/game';
import { Player } from '../../models/player';
import { Word } from '../../models/Word';
import { HttpErrorResponse } from '@angular/common/http';

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
  errMessage: string = '';
  score: number;
  wordsCounter: string;
  listEnable: boolean = false;
  gameOver: boolean = false;
  gameCreated: boolean = false;
  timeLeft: number = 180;


  wordsArray: Word[] = [];

  public CLearBtnDisable: boolean;

  constructor(private playerService: PlayerService, private gameService: GameService, private authService: AuthService) {
    this.CuttedLetters = {};
    this.resultWord = [];
    this.score = 0;
    this.wordsCounter = '0';
  }

  ngOnInit() {
    this.loadPlayer();
    // interval(1000).pipe(map((x) => {
    //   this.timeLeft--;
    //   if (this.timeLeft === 0) {
    //     this.gameOver = true;
    //   }
    // }));
  }
  errMessageClear() {
    this.errMessage = '';
  }

  loadPlayer() {
    if (this.authService.isTokenValid()) {
      var temp = this.authService.getValueFromIdToken('sub');
      this.playerService.getPlayer(temp).subscribe(p => {
        this.player = p;
        if (this.player !== null) {
          this.loadGame(this.player.id);
        }
      });
    }
  }

  loadGame(playerId: string) {
    if (this.authService.isTokenValid()) {
      this.gameService.getGame(playerId).subscribe(p => {
        this.game = p;
        this.keyWord = this.game.keyWord;
        this.keyWordLetters = this.keyWord.split('');
        this.gameService.getAllWords(this.game.id).subscribe(p => {
          this.wordsArray = p;
          this.wordsCounter = this.wordsArray.length.toString();
        });
        this.gameCreated = true;
      }, err => {
        this.createGame(playerId);
      });
    }
  }

  createGame(playerId: string) {
    this.gameService.createGame(playerId).subscribe(p => {
      this.game = p;
      this.keyWord = this.game.keyWord;
      this.keyWordLetters = this.keyWord.split('');
      this.gameCreated = true;
    });
  }

  CellClick(index: number) {
    this.errMessageClear();
    log(index.toString());
    if (this.keyWordLetters[index] === '_') {
      //this.keyWordLetters[index] = this.CuttedLetters[index];
      //this.CuttedLetters[index] = null;
      //this.resultWord.pop();
    }
    else {
      this.CuttedLetters[index] = this.keyWordLetters[index];
      this.resultWord.push(this.keyWordLetters[index]);
      this.keyWordLetters[index] = '_';
      if (this.resultWord.join('') === this.keyWord) {
        this.ClearBtnClick();
        this.errMessage = "Нельзя вводить ключевое слово";
      }
    }
  }
  ClearBtnClick() {
    this.keyWordLetters = this.keyWord.split('');
    this.resultWord.length = 0;
    this.CuttedLetters = {};
  }
  ListBtnClick() {
    if (this.listEnable) {
      this.listEnable = false;
    }
    else {
      this.listEnable = true;
    }
  }
  SubmitBtnClick() {
    const word = this.resultWord.join('');
    if (word !== '') {
      this.gameService.submitWord(word, this.game.id).subscribe(p => {
        this.wordsArray.push(p);
        this.score += p.value.length;
        this.wordsCounter = this.wordsArray.length.toString();
        this.wordsArray.forEach(element => {
          log("Returned word - " + element.value + ", ");
          this.ClearBtnClick();
          this.errMessage = "Верно!";
        });
      }, err => {
        const msg = (err as HttpErrorResponse).error;
        if (msg == "This word already exists(Word Handler)") {
          this.errMessage = "Ошибка! Такое слово уже есть.";
        }
        else if (msg == "This word incorrect (Word Handler)") {
          this.errMessage = "Ошибка! Такого слова не существует.";
        }
        this.ClearBtnClick();
      });
    }
  }

  CheckResultWord() {
    return this.resultWord.length > 0 ? false : true;
  }
}
