import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loose-game',
  templateUrl: './loose-game.component.html',
  styleUrls: ['./loose-game.component.sass']
})
export class LooseGameComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  startGame() {
    this.router.navigateByUrl('/gameSession');
  }
}
