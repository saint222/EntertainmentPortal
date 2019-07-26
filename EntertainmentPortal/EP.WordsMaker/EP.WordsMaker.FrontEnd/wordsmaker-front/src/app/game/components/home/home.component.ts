import { Router } from '@angular/router';
import { GameService } from './../../services/game.service';
import { log } from 'util';
import { LoginComponent } from './../login/login.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../../services/player.service';
import { AuthService } from './../../services/auth.service';
import { Player } from '../../models/player';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  userGroup: FormGroup;
  playerName: string;


  login:boolean = false;
  playerCreated:boolean = false;
  player:Player;
  returnedPlayer: Player = null;

  constructor(private fb: FormBuilder, private playerService: PlayerService, private authService: AuthService, private router: Router ) {
    this.authService.tokenValidState.subscribe(e=>{
      log("Token event reseived in HOME Component");
      this.updateComponent();
    });
    this.userGroup = this.fb.group({
      name: [this.playerName, [Validators.required, Validators.minLength(4)]]
    });
  }
  ngOnInit() {
    this.updateComponent();

  }

  updateComponent()
  {
    if(this.authService.isTokenValid())
    {
      this.login = true;
      var temp = this.authService.getValueFromIdToken("sub");
      this.playerService.getPlayer(temp).subscribe(
        p => {
          this.returnedPlayer = p;
          if(this.returnedPlayer !== null)
          {
            this.playerCreated = true;
          }
        },
        () =>{
          this.playerCreated = false;
        });
    }
  }


  createPlayer(name: string)
  {
    this.playerService.addPlayer(name, this.authService.getValueFromIdToken("sub"), this.authService.getValueFromIdToken("email")).subscribe(
      p => {
              this.player = p;
            });
  }

  createGame()
  {
    this.router.navigateByUrl("game");
  }
}
