
import { Component } from '@angular/core';
import { DeckService } from './pyatnashki/services/deck.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'pyatnashki';

  constructor() {}

}
