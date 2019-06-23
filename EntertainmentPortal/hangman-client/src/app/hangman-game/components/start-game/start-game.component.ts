import { Component, OnInit} from '@angular/core';
import { GameData } from '../../models/game-data';
import { GameService } from '../../services/game.service';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.sass']
})
export class StartGameComponent implements OnInit {
  gameDataStart: GameData = null;
  constructor(private gameService: GameService) { }

  ngOnInit() {
  }
  click() {
    this.gameService.createGame().subscribe(b => {
      this.gameDataStart = b;
    },
      (err: HttpResponseBase) => console.log(err.statusText));
  }
}
