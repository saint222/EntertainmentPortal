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

  click() {
    this.router.navigateByUrl('/gameSession');
  }

}
