import {Component, Injectable, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class BoardComponent implements OnInit {

  board: any[] = new Array(1);
  boardLocked: boolean;

  message: number;
  mapSize: number;
  userId: string;
  cells: string[] = new Array(1);
  isMap = false;

  constructor(private share: ShareService) {
  }

  ngOnInit(): void {
    // this.share.onMapClick.subscribe(size => this.mapSize = size);
    this.share.currentMessage.subscribe(message => this.mapSize = message);
    this.updateComponent();
  }

  square_click(square) {
    if (square.value === '') { // if square is empty & game is not over
      square.value = 'X';  // assign symbol to square
      // this.completeMove(this.Player1);   // transfer to completeMove method
    }
  }

  updateComponent() {
    if (this.mapSize > 2) {
      this.isMap = true;
      for (let i = 0; i < this.mapSize * this.mapSize; i++) {
        this.cells[i] = '';
      }
      for (let i = 0; i < this.mapSize * this.mapSize; i++) {
        this.board[i] = {value: ''};
      }
    }
  }

}
