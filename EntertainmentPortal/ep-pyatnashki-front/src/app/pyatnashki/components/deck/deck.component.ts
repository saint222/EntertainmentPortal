import { HttpResponseBase } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Deck } from '../../models/deck';
import { DeckService } from '../../services/deck.service';
import { Tile } from '../../models/tile';

@Component({
  selector: 'app-deck',
  templateUrl: './deck.component.html',
  styleUrls: ['./deck.component.scss']
})
export class DeckComponent implements OnInit {
  deck: Deck = new Deck(0, 4, false, [
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
    new Tile(0, 15),
    new Tile(15, 16),

  ]);
  constructor(private deckService: DeckService) { }

  ngOnInit() {
    this.deckService.getDeck().subscribe(d => this.deck = d);
  }

  newDeck() {
    this.deckService.newDeck().subscribe(d => this.deck = d);
  }
  moveTile(num: number) {
    this.deckService.moveTile(num).subscribe(d => this.deck = d);
  }

}
