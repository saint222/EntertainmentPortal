import { CellStatus } from './../../models/cellStatus';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss']
})
export class CellComponent implements OnInit {

  @Input() status: CellStatus;

  constructor() { }

  ngOnInit() {
  }
}
