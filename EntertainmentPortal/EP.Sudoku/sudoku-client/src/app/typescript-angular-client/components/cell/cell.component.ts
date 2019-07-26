import { Cell } from './../../model/cell';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { SessionsService } from '../../api/api';

@Component({
  selector: 'app-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss']
})
export class CellComponent implements OnInit {
  @Input() cell: Cell;

  constructor(private sessionService: SessionsService) { }

  ngOnInit() {
  }

  updateCell(value: string) {
    if ( !isNaN(+value) && typeof +value === 'number') {
      this.cell.value = +value;
    } else {
      this.cell.value = 0;
      this.sessionService.updateSession.next('Invalid data is entered. Numbers from "0" till "9" are expected only!');
      console.log('A letter is entered, you must enter a number!');
      return false;
    }

    this.sessionService.sessionsSetCellValue(this.cell).subscribe(
      s => {
        console.log('subscribe');
        this.sessionService.updateSession.next();
      },
      (err: HttpErrorResponse) => {
        console.log('subscribe Error');
        this.cell.value = 0;
        this.sessionService.updateSession.next(err.error);
        return console.log(err.error);
      }
    );
  }

  getHint() {

    this.sessionService.sessionsGetHint(this.cell).subscribe(
      s => {
        this.cell = s.squares.find(c => c.id === this.cell.id);
        this.sessionService.updateSession.next();
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

  isGrayColor(cell: Cell) {
    if (cell.x >= 1 && cell.x <= 3 && cell.y >= 1 && cell.y <= 3) {
      return true;
    }
    if (cell.x >= 1 && cell.x <= 3 && cell.y >= 7 && cell.y <= 9) {
      return true;
    }
    if (cell.x >= 4 && cell.x <= 6 && cell.y >= 4 && cell.y <= 6) {
      return true;
    }
    if (cell.x >= 7 && cell.x <= 9 && cell.y >= 1 && cell.y <= 3) {
      return true;
    }
    if (cell.x >= 7 && cell.x <= 9 && cell.y >= 7 && cell.y <= 9) {
      return true;
    }

    return false;
  }
}
