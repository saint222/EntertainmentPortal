import { GameAndCells } from './../../models/gameAndCells';
import { GameService } from './../../services/game.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Cell } from '../../models/cell';
import { HttpResponseBase, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.sass']
})
export class MapComponent implements OnInit {
  cells: Cell[][] = [];
  selectedCellsId: Array<number> = new Array<number>();
  selectedCells: Cell[] = [];
  selectedLetters = 'Your word: ';
  id: string;
  alphabet: string[] = [];
  currentLetter: string;
  tempLetter: Cell = new Cell();
  errorText: string;
  words: string[] = [];

  constructor(private gameService: GameService, private route: ActivatedRoute,  private router: Router) {
    this.route.params.subscribe( params => this.gameService.getGame(params.gameid));
  }

  ngOnInit() {
    this.gameService.getMap(this.route.snapshot.queryParamMap.get('mapid')).subscribe(p => {
      this.cells = p;
      this.getPlayersWord();
    },
      (err: HttpErrorResponse) => {
        this.errorText = err.error;
        return console.log(err.statusText);
      });
    this.id = this.route.snapshot.queryParamMap.get('mapid');
    this.gameService.getAlphabet().subscribe(a => {
      this.alphabet = a;
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
    this.selectedCellsId.push(chosenCell.id);
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
    this.selectedCells.forEach(element => {
      element.checked = false;
    });
    this.selectedCellsId = [];
    this.selectedCells = [];
    this.selectedLetters = 'Your word: ';
   }

   selectLetter(letter: string) {
      this.currentLetter = letter;
   }

   getPlayersWord() {
     this.gameService.getPlayersWords(this.route.snapshot.queryParamMap.get('gameId'))
     .subscribe(w => this.words = w);
   }

   isGameOver(cells: Cell[][]) {
     let isOver = true;
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
 }
