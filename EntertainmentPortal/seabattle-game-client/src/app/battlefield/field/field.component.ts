import { ShootService } from './../services/shoot.service';
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
  alphabet: string[];
  numbers: number[];
  N = 10;

  constructor(public battleFieldService: BattlefieldService, public shootService: ShootService) {
    this.alphabet = [ ' ', '0', '1', '2', '3', '4', '5', '6' , '7', '8', '9' ];
    this.numbers = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];
   }

  ngOnInit() {
  }

  shoot(cell: Cell) {
    this.shootService.addShot(cell.y, cell.x, '1', '1');
  }

  reset() {
    this.battleFieldService.reset();
    this.shootService.reset();
  }
}
