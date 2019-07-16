import { GameService } from './../../services/game.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-start-screen',
  templateUrl: './start-screen.component.html',
  styleUrls: ['./start-screen.component.sass']
})
export class StartScreenComponent implements OnInit {

  constructor(private router: Router, private gameService: GameService) {
   }

  ngOnInit() {
  }

  startGame() {
    this.router.navigateByUrl('/gameSession');
  }

  loginUser() {
    this.gameService.loginUser();
  }

  registerUser() {
    this.gameService.registerUser().subscribe(b => {
      this.loginUser();
    },
    (err: HttpResponseBase) => {
      console.log(err.statusText);
      this.router.navigateByUrl('/');
    });
  }
}
