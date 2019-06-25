import { Cell } from './../../model/cell';
import { Component, OnInit } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';
import { Session } from '../../model/models';
import { SessionsService } from '../../api/api';

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.scss']
})
export class SessionComponent implements OnInit {

  session?: Session;
  cells: Cell[] = [];
  cell: Cell;

  constructor(private sessionService: SessionsService) { }

  ngOnInit() {
    this.getSessionById();
  }

  getSessionById(){
    this.sessionService.sessionsGetSessionById(1).subscribe(
      s => {
        this.session = s;
        this.cells = s.squares;
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      }
    );
  }
}
