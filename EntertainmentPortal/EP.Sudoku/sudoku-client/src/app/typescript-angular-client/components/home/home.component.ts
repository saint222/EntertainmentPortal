import { AuthService } from './../../api/auth.service';
import { Component, OnInit} from '@angular/core';
import { PlayersService } from '../../api/players.service';
import { Player } from '../../model/player';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  player?: Player;
  logged: boolean;
  hasProfile: boolean;

  constructor(private playersService: PlayersService, private authService: AuthService) {
    this.updateComponent();

    this.authService.tokenValidState.subscribe(e => {
      this.updateComponent();
    });
  }

  ngOnInit() {}

  updateComponent() {
    if (this.authService.isTokenValid()) {
      this.logged = this.authService.isTokenValid();

      this.playersService.playersGetPlayerByUserId().subscribe( () => {
        this.hasProfile = true;
      }, () => {
        this.hasProfile = false;
      });
    }
  }
}
