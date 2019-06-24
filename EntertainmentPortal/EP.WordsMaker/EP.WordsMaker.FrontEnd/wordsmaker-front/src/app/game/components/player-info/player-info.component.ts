import { PlayerService } from './../../services/player.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { Player } from '../../models/player';

@Component({
  selector: 'app-player-info',
  templateUrl: './player-info.component.html',
  styleUrls: ['./player-info.component.scss']
})
export class PlayerInfoComponent implements OnInit {

@Input() player: Player;

  constructor() { }

  ngOnInit() {
  }

}
