import { Cell } from './../../models/cell';
import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../services/session.service';
import { HttpResponseBase } from '@angular/common/http';
import { Session } from '../../models/session';

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.scss']
})
export class SessionComponent implements OnInit {

  session?: Session;
  cells: Cell[] = [];
  cell: Cell;
  
  constructor(private sessionService: SessionService) { }

  ngOnInit() {
    this.getSessionById();
  }

  getSessionById(){
    this.sessionService.getSessionById(1).subscribe(
      s => {
        this.session = s;
        console.log(s);
        this.cells = s.squares;
      },
      (err: HttpResponseBase) => {
        return console.log(err.statusText);
      }
    );  
  }
}
