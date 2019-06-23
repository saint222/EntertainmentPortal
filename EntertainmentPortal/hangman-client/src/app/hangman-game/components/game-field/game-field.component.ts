import { GameData } from './../../models/game-data';
import { GameService } from './../../services/game.service';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-game-field',
  templateUrl: './game-field.component.html',
  styleUrls: ['./game-field.component.sass']
})
export class GameFieldComponent implements OnInit {
  @Input() gameFieldData: GameData = null;
  @Output() updatedGameData = new EventEmitter();


  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  updateField(newData) {
    this.updatedGameData.emit(newData);
  }
}
