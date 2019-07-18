import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-win-game',
  templateUrl: './win-game.component.html',
  styleUrls: ['./win-game.component.sass']
})
export class WinGameComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  startGame() {
    this.router.navigateByUrl('/gameSession');
  }
}
