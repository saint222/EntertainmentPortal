import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubService {

  private hub: HubConnection;
  tryShot: Function;
  messages: string[] = [];
  readyToSend: boolean = false;
  gameID: string;

  constructor() {
    this.hub = new HubConnectionBuilder().withUrl('https://localhost:44362/sea-battle-2019').build();
    this.hub.start().then(() => {
      this.readyToSend = true;
      if(this.gameID !== null && this.gameID != undefined && this.gameID != "")
      {
        this.hub.send('Subscribe', this.gameID);
      }

    })
    .catch(err => console.error(`Error for hub connection ${err}`));
    this.hub.on('sendShot', (userID: string, x: number, y: number) => {
      this.tryShot(userID, x, y);
    });
   }

   SubscribeToGetEnemyShot(func: Function)
   {
    this.tryShot = func;
   }
   Subscribe(gameID: string){
     if(this.readyToSend)
     {
         this.hub.send('Subscribe', gameID);
     }
     else{
         this.gameID = gameID;
     }
   }
}