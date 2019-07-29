import { HttpErrorResponse } from '@angular/common/http';
import { Player } from './../../model/player';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { PlayersService } from '../../api/api';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-player-info',
  templateUrl: './player-info.component.html',
  styleUrls: ['./player-info.component.scss']
})
export class PlayerInfoComponent implements OnInit {

  @Input() player?: Player;

  constructor(private route: ActivatedRoute, private playersService: PlayersService) {
  }

  ngOnInit() {
    this.route.paramMap
    .pipe(
      switchMap(m => {
        if (m.get('id') != null) {
          return this.playersService.playersGetPlayerById(+m.get('id'));
        }
      })
    )
    .subscribe(result => {
        this.player = result;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
  );
  }

}
