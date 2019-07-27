import { GameService } from './../../../game/services/game.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit {
  http: any;

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }
}
