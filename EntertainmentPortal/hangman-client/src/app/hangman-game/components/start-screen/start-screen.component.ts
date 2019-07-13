import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-start-screen',
  templateUrl: './start-screen.component.html',
  styleUrls: ['./start-screen.component.sass']
})
export class StartScreenComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  startGame() {
    this.router.navigateByUrl('/gameSession');
  }

  loginUser() {
    this.router.navigateByUrl('/login');
  }

  registerUser() {
    this.router.navigateByUrl('/register');
  }
}
