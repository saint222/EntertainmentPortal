import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})

export class BoardComponent implements OnInit {
  mapSize: number;
  userId = '1';

  constructor(private share: ShareService) {
    this.mapSize = 3;
  }

  ngOnInit() {

  }

}
