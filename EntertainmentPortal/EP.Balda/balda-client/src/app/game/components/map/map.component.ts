import { Game } from './../../models/game';
import { GameService } from './../../services/game.service';
import { Router } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.sass']
})
export class MapComponent implements OnInit {
  game: Game;

  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit() {
    this.gameService.currentGame.subscribe(game => this.game = game);
   }

}
