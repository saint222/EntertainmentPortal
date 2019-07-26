import { GameService } from './../../services/game.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Cell } from '../../models/cell';
import { Player } from '../../models/player';
import { CurrentGame } from '../../models/currentGame';
import { HttpResponseBase, HttpErrorResponse } from '@angular/common/http';

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
  gif: any = 'assets/pics/1.gif';

  constructor(private router: Router, private gameService: GameService) { }

  ngOnInit() {
}

    onPlayClick() {
    this.router.navigateByUrl('startGame');
  }
}
