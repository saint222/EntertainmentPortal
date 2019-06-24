import { Component, OnInit } from '@angular/core';
import { Player } from '../../models/player';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.scss']
})
export class PlayerListComponent implements OnInit {

  players: Player[] = [];

  constructor(private playerService: PlayerService) { }

  ngOnInit() {
    this.playerService.getPlayers().subscribe(
      p => {
            this.players = p;
            console.log(p);
            }
    );
  }

}
