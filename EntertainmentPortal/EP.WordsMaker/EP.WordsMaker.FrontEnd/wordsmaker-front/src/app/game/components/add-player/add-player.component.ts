import { Component, OnInit } from '@angular/core';
import { Player } from '../../models/player';
import { PlayerService } from '../../services/player.service';
import { of } from 'rxjs';


@Component({
  selector: 'app-add-player',
  templateUrl: './add-player.component.html',
  styleUrls: ['./add-player.component.scss']
})
export class AddPlayerComponent implements OnInit {

  player: Player = new Player();
  recievedPlayer: Player;

  constructor(private playerService: PlayerService) { }

  ngOnInit() {
  }

  click(player: Player) {
    this.playerService.addPlayer(player).
    subscribe( (p: Player) => {this.recievedPlayer = player}
      );
  }

}
