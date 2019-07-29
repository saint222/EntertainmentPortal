import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.sass']
})
export class StartPageComponent implements OnInit {
  title = 'Balda Game';

  constructor(private router: Router ) { }

  ngOnInit() {
  }
}
