import { Playground } from '../../models/playground';
import { GameService } from './../../services/game.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { Cell } from '../../models/cell';
import { HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.sass']
})
export class MapComponent implements OnInit {
  playground: Playground = new Playground();
  cells: Cell[][] = [];
  id: string;

  constructor(private gameService: GameService, private route: ActivatedRoute,  private router: Router) {
    this.route.params.subscribe( params => this.gameService.getGame(params.gameid));
  }

  ngOnInit() {
    this.gameService.getMap(this.route.snapshot.queryParamMap.get('mapid')).subscribe(p => {
      // this.playground = p;
      this.cells = p;
    },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      });
    this.id = this.route.snapshot.queryParamMap.get('mapid');
   }
 }
