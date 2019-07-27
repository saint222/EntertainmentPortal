import { CurrentGame } from '../../models/currentGame';
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
  mapId: string;
  gameId: string;
  playerId: string;
  alphabet: string[] = [];
  alphabetP1: string[] = [];
  alphabetP2: string[] = [];
  currentLetter: string;
  tempLetter: Cell = new Cell();
  errorText: string;
  playerWords: string[] = [];
  opponentWords: string[] = [];
  isPlayerTurn: boolean;
  isGameOver: boolean;
  playerScore: number;
  opponentScore: number;
  mapWithStatus: CurrentGame = new CurrentGame();

  constructor(private gameService: GameService, private route: ActivatedRoute,  private router: Router) {
   }

  ngOnInit() {
    this.gameService.getCurrentGame().subscribe(m => {
      console.log(m);
      this.cells = m.cells;
      this.gameId = m.gameId;
      this.playerId = m.userId;
      this.opponentScore = m.opponentScore;
      this.playerScore = m.playerScore;
      this.mapId = m.mapId;
      this.isPlayerTurn = m.isPlayersTurn;
      this.gameService.getPlayer(this.playerId).subscribe(p => { this.player = p; },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
      this.gameService.getAlphabet().subscribe(a => {
          this.alphabet = a;
          this.divideAlphabetInParts(this.alphabet);
        },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
      this.gameService.getPlayersWords(this.gameId).subscribe(w => {
          this.playerWords = w;
        },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
      this.gameService.getPlayersOpponentWords(this.gameId).subscribe(w => {
          this.opponentWords = w;
        },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
    },
    (err => this.router.navigateByUrl('startGame')));
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
    gameAndCells.CellsThatFormWord = this.selectedCells;
    gameAndCells.gameId = this.gameId;
    this.gameService.sendWord(gameAndCells).subscribe(w => {
      console.log(w);
      this.isGameOver = w.isGameOver;
      this.opponentScore = w.opponentScore;
      this.playerScore = w.playerScore;
      this.isPlayerTurn = w.isPlayersTurn;
      this.gameService.getMap(this.mapId).subscribe(m => {
        this.cells = m;
      });
      if (this.isGameOver) {
        this.router.navigateByUrl('gameOver');
      }
      if (this.isPlayerTurn) {
        this.gameService.getPlayersOpponentWords(this.gameId).subscribe(p => {
          this.opponentWords = p;
        },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
      } else {
        this.gameService.getPlayersWords(this.gameId).subscribe(w2 => {
          this.playerWords = w2;
        },
        (err: HttpResponseBase) => {
          return console.log(err.statusText);
        });
      }
    },
    (err: HttpErrorResponse) => {
      this.errorText = err.error;
      setTimeout(() => {
        this.errorText = '';
    }, 5000);
      return console.log(err.statusText);
    });

    this.onCancelClick();
   }

   onCancelClick() {
    this.currentLetter = '';
    this.tempLetter.letter = '';
    this.selectedCells.forEach(element => {
      element.checked = false;
    });
    this.selectedCells = [];
    this.selectedLetters = '';
   }

   selectLetter(letter: string) {
      this.currentLetter = letter;
   }

   divideAlphabetInParts(alph: string[]) {
    for (let i = 0; i < 13; i++) {
      this.alphabetP1.push(alph[i]);
    }
    for (let i = 13; i < 26; i++) {
      this.alphabetP2.push(alph[i]);
    }
   }

   onLeaveButton() {
      this.gameService.leaveGame(this.gameId).subscribe(g => {
          console.log(g);
          this.router.navigateByUrl('startGame');
        });
   }
}
