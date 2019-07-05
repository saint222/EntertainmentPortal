import { CellStatus } from './../../models/cellStatus';
import { Cell } from './../../models/cell';
import { Component, OnInit } from '@angular/core';

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
  constructor() {
    this.alphabet = [ ' ', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж' , 'З', 'И', 'К' ];
    this.numbers = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];
   }

  ngOnInit() {
    this.createShipField();
    this.createShootField();
  }

  createShipField() {
    this.shipField = [];

    for (let i = 0; i < this.N; i++) {
        this.shipField[i] = [];
        for (let j = 0; j < this.N; j++) {
            this.shipField[i][j] = new Cell(i, j, CellStatus.None);
        }
    }
  }

  createShootField() {
    this.shootField = [];
    for (let i = 0; i < this.N; i++) {
        this.shootField[i] = [];
        for (let j = 0; j < this.N; j++) {
            this.shootField[i][j] = new Cell(i, j, CellStatus.ShootWithoutHit);
        }
    }
  }
}
