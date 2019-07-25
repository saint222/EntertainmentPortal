import { ChatMessage } from './../../model/chatMessage';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Cell } from './../../model/cell';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Session } from '../../model/models';
import { SessionsService } from '../../api/api';
import { ActivatedRoute, Router } from '@angular/router';
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
  error: string;
  message = '';
  chatMessage: ChatMessage = { name: '', message: ''};
  messages: ChatMessage [] = [];
  hubConnection: HubConnection;


  constructor(private route: ActivatedRoute, private sessionService: SessionsService) {

    this.sessionService.UpdateSession.subscribe(s => {
      this.error = s;
      this.sessionService.sessionsGetSessionById(this.session.id).subscribe(x => {
        this.session = x;
        this.cells = x.squares;
      },
      (err: HttpErrorResponse) => {
        return console.log(err.error);
      });
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
    this.hubConnection = new HubConnectionBuilder().withUrl('https://localhost:44332/sudoku').build();
    this.hubConnection.on('SendMes', (msg) => {
      this.messages.push(msg);
    }); // 'Send' - is the method, defined in the "Clients.All.SendAsync" as an argument in SudokuHub
    this.hubConnection.start()
    .then(() => console.log('Connection started...'));
  }

  sendMsg() {
    this.chatMessage.name = this.getValueFromIdToken('name');
    this.chatMessage.message = this.message;
    this.hubConnection.invoke('GetMes', this.chatMessage); // 'GetMes' - is the method, defined in SudokuHub
  }

  getValueFromIdToken(claim: string) {
    const jwt = sessionStorage.getItem('id_token');
    if ( jwt == null) {
      return null;
    }

    const jwtData = jwt.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    let value: any;
    JSON.parse(decodedJwtJsonData, function findKey(k, v) {
      if (k === claim) {
        value = v;
      }
    });
    return value;
  }
}
