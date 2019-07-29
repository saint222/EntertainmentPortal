import { PlayingFieldComponent } from './../playing-field/playing-field.component';
import { HomeComponent } from './../home/home.component';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {




  constructor(private plFieldComponent: PlayingFieldComponent) {
    this.plFieldComponent.gameUpdateEvent.subscribe(e => {

   });
  }

  ngOnInit() {

  }

}
