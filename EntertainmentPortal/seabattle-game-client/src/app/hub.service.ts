import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  private hub: HubConnection;
  private hubGeneric: HubConnection;

  messages: string[] = [];

  constructor() {
    this.hub = new HubConnectionBuilder().withUrl('https://localhost:6001/sea-battle-2019').build();
    this.hubGeneric = new HubConnectionBuilder().withUrl('https://localhost:6001/demo-generic').build();
    this.hub.start().then(() => console.log('Hub connected'))
    .catch(err => console.error(`Error for hub connection ${err}`));
    this.hubGeneric.start().then(() => console.log('Hub connected'))
    .catch(err => console.error(`Error for hub connection ${err}`));

    this.hub.on('getMessage', (msg: string) => {
      this.messages.push(msg);
      return console.log(`Message: ${msg}`);
    });
    this.hubGeneric.on('DoSomething2', (status: number) => {
      console.log(`Status: ${status}`);
    });
   }

   callServer(){
     this.hub.send('CalledFromClient2', 'Hello from Client', 100500);
   }
}