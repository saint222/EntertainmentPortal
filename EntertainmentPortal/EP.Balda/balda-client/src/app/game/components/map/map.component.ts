import { CreateNewGame } from './../../models/createNewGame';
import { GameService } from './../../services/game.service';
import { Router } from '@angular/router';
import { Cell } from './../../models/cell';
import { Component, OnInit } from '@angular/core';

import { CellService } from '../../services/cell.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.sass']
})
export class MapComponent implements OnInit {
  cells: Cell[] = [];

  constructor(private gameService: GameService, private cellService: CellService, private router: Router) {}

  ngOnInit() {

    this.gameService.createNewGame();

    this.cellService.getCells().subscribe(
      c => {
        this.cells = c;
      }
    );
  }
}
