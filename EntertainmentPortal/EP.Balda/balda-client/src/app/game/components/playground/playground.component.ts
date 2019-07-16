import { Player } from './../../models/player';
import { Component, OnInit } from '@angular/core';
import { Game } from '../../models/game';
import { GameService } from '../../services/game.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.sass']
})
export class PlaygroundComponent implements OnInit {
  game: Game = new Game();
  id: string;
  player: Player = new Player();

  constructor(private gameService: GameService, private route: ActivatedRoute,  private router: Router) {
    this.route.params.subscribe( params => this.gameService.getGame(params.gameid));
   }

  ngOnInit() {
    this.gameService.getGame(this.route.snapshot.queryParamMap.get('gameId')).subscribe(g => { this.game = g; },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
    this.id = this.route.snapshot.queryParamMap.get('gameId');

    this.gameService.getPlayer(this.route.snapshot.queryParamMap.get('userId')).subscribe(p => { this.player = p; },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
  }
}
