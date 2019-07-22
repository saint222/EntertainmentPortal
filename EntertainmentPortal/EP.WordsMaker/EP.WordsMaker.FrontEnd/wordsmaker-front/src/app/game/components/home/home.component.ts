import { log } from 'util';
import { LoginComponent } from './../login/login.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../../services/player.service';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  login:boolean = false;

  constructor(private playerService: PlayerService, private authService: AuthService ) {
    this.authService.tokenValidState.subscribe(e=>{
      log("Token event reseived in HOME Component");
      this.updateComponent();
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
    }
  }
}
