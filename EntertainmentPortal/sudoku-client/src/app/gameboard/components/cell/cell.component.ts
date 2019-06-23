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
    console.log(cell);
    console.log(value);
    this.cell.value = value;

    this.sessionService.updateCell(this.cell).subscribe(
      s => { 
        console.log(s);
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      }
    );

  }
}
