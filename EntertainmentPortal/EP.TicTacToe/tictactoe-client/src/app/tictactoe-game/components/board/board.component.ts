import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})

export class BoardComponent implements OnInit {
  private mapSize: number;
  private userId: string;

  constructor(private share: ShareService) {
    this.share.onMapClick.subscribe(size => this.mapSize = size);
  }

  ngOnInit() {

  }

}
