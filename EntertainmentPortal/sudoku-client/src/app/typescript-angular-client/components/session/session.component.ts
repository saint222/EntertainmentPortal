import { Cell } from './../../model/cell';
import { Component, OnInit } from '@angular/core';
import { HttpResponseBase, HttpErrorResponse } from '@angular/common/http';
import { Session } from '../../model/models';
import { SessionsService } from '../../api/api';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.scss']
})
export class SessionComponent implements OnInit {

  session?: Session;
  cells: Cell[] = [];
  cell: Cell;

  constructor(private route: ActivatedRoute, private sessionService: SessionsService) {
    this.sessionService.UpdateHint.subscribe(s => this.session.hint = this.session.hint - 1);
    this.sessionService.NewSession.subscribe(s => {
      this.sessionService.sessionsGetSessionById(+s).subscribe(x => {
          this.session = x;
          this.cells = x.squares;
        },
        (err: HttpErrorResponse) => {
          return console.log(err.error);
        }
      );
    });

    this.route.paramMap
      .pipe(
        switchMap(m => {
          return this.sessionService.sessionsGetSessionById(+m.get('id'));
        })
      )
      .subscribe(result => {
          this.session = result;
          this.cells = result.squares;
        },
        (err: HttpErrorResponse) => {
          return console.log(err.error);
        }
      );
   }

  ngOnInit() {
  }

  getSessionById(){
    this.sessionService.sessionsGetSessionById(1).subscribe(
      s => {
        this.session = s;
        this.cells = s.squares;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      }
    );
  }
}
