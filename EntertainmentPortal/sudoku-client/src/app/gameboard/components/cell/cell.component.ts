import { HttpResponseBase } from '@angular/common/http';
import { Cell } from './../../models/cell';
import { Component, OnInit, Input } from '@angular/core';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss']
})
export class CellComponent implements OnInit {
  @Input() cell: Cell;

  constructor(private sessionService: SessionService) { }

  ngOnInit() {
  }

  updateCell(cell: Cell, value: number){

    console.log(value);
    this.cell.value = value;

    this.sessionService.updateCell(this.cell).subscribe(
      s => { 
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      }
    );
  }

  isGrayColor(cell: Cell){
    if (cell.x >= 1 && cell.x <= 3 && cell.y >= 1 && cell.y <= 3)
      return true;
    if (cell.x >= 1 && cell.x <= 3 && cell.y >= 7 && cell.y <= 9)
      return true;
    if (cell.x >= 4 && cell.x <= 6 && cell.y >= 4 && cell.y <= 6)
      return true;
    if (cell.x >= 7 && cell.x <= 9 && cell.y >= 1 && cell.y <= 3)
      return true;
    if (cell.x >= 7 && cell.x <= 9 && cell.y >= 7 && cell.y <= 9)
      return true;

    return false;
  }
}
