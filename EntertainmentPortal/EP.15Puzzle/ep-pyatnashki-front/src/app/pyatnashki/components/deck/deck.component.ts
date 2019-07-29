import { NotifyHubService } from './../../services/notify-hub.service';
import { AccountService } from 'src/app/account/services/account.service';
import { ConfigService } from './../../../shared/services/config.service';
import { Component, OnInit } from '@angular/core';
import { Deck } from '../../models/deck';
import { DeckService } from '../../services/deck.service';
import { Tile } from '../../models/tile';

@Component({
  selector: 'app-deck',
  templateUrl: './deck.component.html',
  styleUrls: ['./deck.component.scss'],
  providers: [DeckService, ConfigService]
})
export class DeckComponent implements OnInit {
  deck: Deck;


  // tslint:disable-next-line: no-inferrable-types
  logged: boolean = true;
  constructor(private deckService: DeckService, public accountService: AccountService, public notifyHubService: NotifyHubService) { }

  ngOnInit() {
    if (this.accountService.IsLoggedIn()) {
      const d = sessionStorage.getItem('deck');
      if (d != null) {
        this.deck = JSON.parse(d);
      } else {
        // tslint:disable-next-line: no-shadowed-variable
        this.deckService.getDeck().subscribe(d => {this.deck = d; });
        if (this.deck == null) {
          this.deck = this.getNewDeck();
        }

      }
    } else {
      this.deck = this.getNewDeck();
      this.logged = false;
    }
  }
  isLoggedIn() {
    return this.accountService.IsLoggedIn();
  }
  getDeck() {
    this.deckService.getDeck().subscribe(d => {
      this.deck = d;
      this.save('deck', d);
    });
  }
  newDeck() {
    this.deckService.newDeck().subscribe(d => {
      this.deck = d;
      this.save('deck', d);
    });
  }
  moveTile(num: number) {
    if (!this.deck.victory) {
      this.deckService.moveTile(num).subscribe(d => {
          this.deck = d as Deck;
          this.save('deck', this.deck);
      }, error => this.notifyHubService.notify('This tile can not be moved'));
    }
  }

  private getNewDeck() {
    return new Deck(0, 4, false, [
      new Tile(1, 1),
      new Tile(2, 2),
      new Tile(3, 3),
      new Tile(4, 4),
      new Tile(5, 5),
      new Tile(6, 6),
      new Tile(7, 7),
      new Tile(8, 8),
      new Tile(9, 9),
      new Tile(10, 10),
      new Tile(11, 11),
      new Tile(12, 12),
      new Tile(13, 13),
      new Tile(14, 14),
      new Tile(15, 15),
      new Tile(0, 16),
    ]);
  }
  private save(name: string, item: Deck) {
    sessionStorage.setItem(name, JSON.stringify(item));
  }
}
