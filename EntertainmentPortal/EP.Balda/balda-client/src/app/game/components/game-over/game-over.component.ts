import { GameService } from './../../services/game.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Cell } from '../../models/cell';
import { Player } from '../../models/player';
import { CurrentGame } from '../../models/currentGame';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-game-over',
  templateUrl: './game-over.component.html',
  styleUrls: ['./game-over.component.sass']
})
export class GameOverComponent implements OnInit {

  gameId: string;
  player: Player = new Player();
  mapid: string;
  gameid: string;
  playerId: string;
  alphabet: string[] = [];
  alphabetP1: string[] = [];
  alphabetP2: string[] = [];
  playerWords: string[] = [];
  opponentWords: string[] = [];
  playerScore: number;
  opponentScore: number;

  constructor(private router: Router, private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getCurrentGame().subscribe(g => this.gameId = g.gameId);
    this.gameService.getGame(this.gameId).subscribe(g => {
      this.gameService.getCurrentGame().subscribe(m => {
        console.log(m);
        this.gameid = m.gameId;
        this.playerId = m.userId;
        this.gameService.getPlayer(this.playerId).subscribe(p => { this.player = p; },
          (err: HttpResponseBase) => {
            return console.log(err.statusText);
          });
        this.gameService.getPlayersWords(this.gameid).subscribe(w => {
            this.playerWords = w;
          },
          (err: HttpResponseBase) => {
            return console.log(err.statusText);
          });
        this.gameService.getPlayersOpponentWords(this.gameid).subscribe(w => {
            this.opponentWords = w;
          },
          (err: HttpResponseBase) => {
            return console.log(err.statusText);
          });
      },
      (err => this.router.navigateByUrl('startGame')));
  });
}

  onPlayClick() {
    this.router.navigateByUrl('startGame');
  }
}
