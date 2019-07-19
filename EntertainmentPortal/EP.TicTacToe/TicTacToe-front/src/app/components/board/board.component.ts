import { Component, OnInit } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';

import { BoardService } from './../../services/board.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  Player2 = { name: 'Opponent', symbol: 'o' };
  Player1 = { name: 'You', symbol: 'x' };
 
  board: any[];
  cells: number[];
  currentPlayer = this.Player1;
  lastWinner: any;
  gameStatus: number;
  boardLocked: boolean;

  constructor(private bookService: BoardService) { }

  ngOnInit() {
    this.newGame();
    
  }

  square_click(square) {
    if(square.value === '' && this.gameStatus === 0) { // if square is empty & game is not over
      square.value = this.Player1.symbol;  // assign symbol to square
      this.completeMove(this.Player1);   // transfer to completeMove method
    }
  }

  completeMove(player) {
    if(this.isWinner(player.symbol)) // check if there is a winner
      this.showGameOver(player);
    else if(!this.availableSquaresExist()) // check if there is an empty square exist      
      this.gameStatus = 2;
    else {
      this.currentPlayer = (this.currentPlayer == this.Player2 ? this.Player1 : this.Player2); 
      // change current player

      if(this.currentPlayer == this.Player2)  
        this.opponentMove();
    }
  }

  showGameOver(winner) {
    this.gameStatus = 1;
    this.lastWinner = winner;

    //if(winner !== this.Draw)
      this.currentPlayer = winner;  
  }

  opponentMove(firstMove: boolean = false) {
    this.boardLocked = true;

    setTimeout(() => {
      let square = firstMove ? this.board[4] : this.getRandomAvailableSquare();
      square.value = this.Player2.symbol;
      this.completeMove(this.Player2);
      this.boardLocked = false;
    }, 600);
  }
 
  availableSquaresExist(): boolean {
    return this.board.filter(s => s.value == '').length > 0;
  }

  getRandomAvailableSquare(): any { // to be deleted
    let availableSquares = this.board.filter(s => s.value === '');
    var squareIndex = this.getRndInteger(0, availableSquares.length - 1);

    return availableSquares[squareIndex];
  }  

  get winningIndexes(): any[] {
    return [
      [0, 1, 2],  //top row
      [3, 4, 5],  //middle row
      [6, 7, 8],  //bottom row
      [0, 3, 6],  //first col
      [1, 4, 7],  //second col
      [2, 5, 8],  //third col
      [0, 4, 8],  //first diagonal
      [2, 4, 6]   //second diagonal
    ];
  }

  isWinner(symbol): boolean {
    for(let pattern of this.winningIndexes) {
      const foundWinner = this.board[pattern[0]].value == symbol
        && this.board[pattern[1]].value == symbol
        && this.board[pattern[2]].value == symbol;

      if(foundWinner) {
        for(let index of pattern) {
          this.board[index].winner = true;
        }

        return true;
      }
    }

    return false;
  }

  newGame() {    
    this.board = [
      { value: '' }, { value: '' }, { value: '' },
      { value: '' }, { value: '' }, { value: '' },
      { value: '' }, { value: '' }, { value: '' }
    ];

    this.gameStatus = 0;
    this.boardLocked = false;

    if(this.currentPlayer == this.Player2){ // if Player2 won last game, Player would start new game
      this.boardLocked = true;
      this.opponentMove(true);  
    }
  }

  transferCellsToBoard() {    
    for (var i = 0; i < this.cells.length; i++) 
      for (var j = 0; i < this.board.length; j++)
        if (this.cells[i] == 0)
          this.board[j] = '';
        if (this.cells[i] == 1)
          this.board[j] = 'x';
        if (this.cells[i] == -1)
          this.board[j] = 'o';
  }

  getRndInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1) ) + min;
  }   
}
