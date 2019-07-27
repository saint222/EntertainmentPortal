import { GameService } from './../../services/game.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-over',
  templateUrl: './game-over.component.html',
  styleUrls: ['./game-over.component.sass']
})
export class GameOverComponent implements OnInit {

  playerScore: string;
  opponentScore: string;
  playerName: string;
  textCongrats: string;
  gif: any = 'assets/pics/1.gif';

  constructor(private router: Router, private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getResults().subscribe(r => {
      this.opponentScore = r.opponentScore;
      this.playerScore = r.playerScore;
      this.playerName = r.playerName;
      if (Number(this.playerScore) > Number(this.opponentScore)) {
          this.textCongrats = this.playerName + ' WINS!!!';
       }
      if (Number(this.opponentScore) > Number(this.playerScore)) {
          this.textCongrats = 'Opponent WINS!!!';
      }
      if (Number(this.opponentScore) === Number(this.playerScore)) {
        this.textCongrats = 'DRAUGHT!';
      }
      });
}

    onPlayClick() {
    this.router.navigateByUrl('startGame');
  }
}
