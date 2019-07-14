import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SudokuHubService {

  private hub: HubConnection;
  messages: string[] = [];

  constructor() {
this.hub = new HubConnectionBuilder().withUrl('https://localhost:44332/sudoku').build();
this.hub.on('getMessage', (msg: string) => {
  this.messages.push(msg);
  return console.log(`Message: ${msg}`);
});
  }
  callServer() {
    this.hub.send('CalledFromClient', 'Hello from Client');
  }
}
