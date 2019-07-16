import { CellStatus } from './../../models/cellStatus';
import { Cell } from './../../models/cell';
import { Component, OnInit } from '@angular/core';
import { BattlefieldService } from '../services/battlefield.service';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrls: ['./field.component.scss']
})
export class FieldComponent implements OnInit {
  shipField: Cell[][];
  shootField: Cell[][];
  alphabet: String[];
  numbers: number[];
  N = 10;

  constructor(public battleFieldService: BattlefieldService) {
    this.alphabet = [ ' ', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж' , 'З', 'И', 'К' ];
    this.numbers = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];
   }

  ngOnInit() {
    this.shipField = this.battleFieldService.createField();
    this.shootField = this.createField();
  }

  createField(): Cell[][] {
    let field = [];
    for (let i = 0; i < this.N; i++) {
      field[i] = [];
      for (let j = 0; j < this.N; j++) {
          field[i][j] = new Cell(i, j, CellStatus.None);
      }
    }
    return field;
  }

  shoot(cell: Cell) {
    console.log(`X = ${cell.x} Y = ${cell.y} Status = ${cell.status}`);
    cell.status = CellStatus.ShootWithoutHit;
  }
}
