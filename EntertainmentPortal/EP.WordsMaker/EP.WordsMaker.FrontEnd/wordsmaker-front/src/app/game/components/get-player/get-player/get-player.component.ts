import { Component, OnInit } from '@angular/core';
import { Player } from 'src/app/game/models/player';
import { PlayerService } from 'src/app/game/services/player.service';

@Component({
  selector: 'app-get-player',
  templateUrl: './get-player.component.html',
  styleUrls: ['./get-player.component.scss']
})
export class GetPlayerComponent implements OnInit {

  id: number;
  player: Player = new Player();
  done: boolean = false;

  constructor(private playerService: PlayerService) { }

  submit(num: number){
    this.playerService.getPlayer(num).subscribe(
      (data: Player) => {
        this.player = data;
        this.done = true;
      });
  }

  ngOnInit() {
  }

}
