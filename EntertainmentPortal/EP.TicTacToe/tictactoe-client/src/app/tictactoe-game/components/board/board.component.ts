import {Component, Injectable, OnInit} from '@angular/core';
import {ShareService} from '../../../core/services/share.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class BoardComponent implements OnInit {

  stringMessage: string;
  message: number;
  mapSize: number;
  userId: string;
  cells: number[] = new Array(1);
  isMap = false;

  constructor(private share: ShareService) {
  }

  ngOnInit(): void {
    // this.share.onMapClick.subscribe(size => this.mapSize = size);
    this.share.currentMessage.subscribe(message => this.mapSize = message);
    this.updateComponent();
  }

  updateComponent() {
    if (this.mapSize > 2) {
      this.isMap = true;
      for (let i = 0; i < this.mapSize * this.mapSize; i++) {
        this.cells[i] = i;
      }
    }
  }

}
