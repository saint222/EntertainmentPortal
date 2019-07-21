import { Player } from './../../models/player';
import { Component, OnInit } from '@angular/core';
import { GameService } from '../../services/game.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpResponseBase, HttpErrorResponse } from '@angular/common/http';
import { Cell } from '../../models/cell';
import { GameAndCells } from '../../models/gameAndCells';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.sass']
})
export class PlaygroundComponent implements OnInit {
  player: Player = new Player();
  cells: Cell[][] = [];
  selectedCells: Cell[] = [];
  selectedLetters = '';
  mapid: string;
  gameid: string;
  alphabet: string[] = [];
  alphabetP1: string[] = [];
  alphabetP2: string[] = [];
  currentLetter: string;
  tempLetter: Cell = new Cell();
  errorText: string;
  words: string[] = [];
  total = 0;

  constructor(private gameService: GameService, private route: ActivatedRoute,  private router: Router) {
   }

  ngOnInit() {
    this.gameService.getPlayer(this.route.snapshot.queryParamMap.get('userId')).subscribe(p => { this.player = p; },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
    this.gameService.getMap(this.route.snapshot.queryParamMap.get('mapId')).subscribe(p => {
        this.cells = p;
        this.getPlayersWord();
      },
        (err: HttpErrorResponse) => {
          this.errorText = err.error;
          return console.log(err.statusText);
        });
    this.mapid = this.route.snapshot.queryParamMap.get('mapId');
    this.gameService.getAlphabet().subscribe(a => {
        this.alphabet = a;
        this.divideAlphabetInParts(this.alphabet);
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
    this.gameid = this.route.snapshot.queryParamMap.get('gameId');
    this.gameService.getPlayersWords(this.gameid).subscribe(w => {
        this.words = w;
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
  }

  selectCell(chosenCell: Cell) {
    if (this.currentLetter != null && chosenCell.letter == null && this.tempLetter.letter == null) {
       chosenCell.letter = this.currentLetter;
       this.tempLetter = chosenCell;
       return;
     }
    if (this.currentLetter != null && chosenCell.letter == null && this.tempLetter.letter != null) {
       this.tempLetter.letter = null;
       chosenCell.letter = this.currentLetter;
       return;
     }
    if (chosenCell.checked === true || chosenCell.letter === null) {
       return;
     }
    this.selectedCells.push(chosenCell);
    this.selectedLetters += chosenCell.letter;
    chosenCell.checked = true;
   }

   onSendClick() {
    const gameAndCells = new GameAndCells();
    gameAndCells.gameId = this.route.snapshot.queryParamMap.get('gameId');
    gameAndCells.CellsThatFormWord = this.selectedCells;
    this.gameService.sendWord(gameAndCells).subscribe(w => {
      this.cells = w;
      if (this.isGameOver(this.cells)) {
        this.router.navigateByUrl('gameover');
      }
      this.getPlayersWord();
    },
    (err: HttpErrorResponse) => {
      this.errorText = err.error;
      return console.log(err.statusText);
    });

    this.onCancelClick();
   }

   onCancelClick() {
    this.currentLetter = '';
    this.selectedCells.forEach(element => {
      element.checked = false;
    });
    this.selectedCells = [];
    this.selectedLetters = '';
   }

   selectLetter(letter: string) {
      this.currentLetter = letter;
   }

   getPlayersWord() {
     this.gameService.getPlayersWords(this.route.snapshot.queryParamMap.get('gameId'))
     .subscribe(w => {
      this.words = w;
      this.total = 0;
      this.words.forEach(word => {
        this.total += word.length;
      });
     });
   }

   isGameOver(cells: Cell[][]) {
     let isOver = true;
     // tslint:disable-next-line: prefer-for-of
     for (let i = 0; i < cells.length; i++) {
       for (let j = 0; j < cells.length; j++) {
         if (cells[i][j].letter == null) {
            isOver = false;
            return isOver;
         }
         continue;
       }
     }
     return isOver;
   }

   divideAlphabetInParts(alph: string[]) {
    for (let i = 0; i < 13; i++) {
      this.alphabetP1.push(alph[i]);
    }
    for (let i = 13; i < 26; i++) {
      this.alphabetP2.push(alph[i]);
    }
   }
}
