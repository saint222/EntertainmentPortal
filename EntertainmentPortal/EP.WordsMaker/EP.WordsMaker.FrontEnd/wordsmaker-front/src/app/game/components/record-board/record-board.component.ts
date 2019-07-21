import { Component, OnInit } from '@angular/core';
import { Player } from '../../models/player';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-record-board',
  templateUrl: './record-board.component.html',
  styleUrls: ['./record-board.component.scss']
})
export class RecordBoardComponent implements OnInit {

  players: Player[] = [];

  constructor(private playerService: PlayerService) { }

  ngOnInit() {
    this.playerService.getPlayers().subscribe(
      p => {
            this.players = p;
            console.log(this.players);
            console.log(p);
            }
    );
  }

}
