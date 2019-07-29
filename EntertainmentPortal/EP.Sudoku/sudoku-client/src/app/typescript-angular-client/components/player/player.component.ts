import { HttpErrorResponse } from '@angular/common/http';
import { PlayersService } from './../../api/players.service';
import { switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { Player } from './../../model/player';
import { Component, OnInit, Input } from '@angular/core';
import { SessionsService } from '../../api/api';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {

  player?: Player;

  constructor(private playersService: PlayersService) {
  }

  ngOnInit() {
    this.playersService.playersGetPlayerByUserId().subscribe(
      b => {
          this.player = b;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }

}
