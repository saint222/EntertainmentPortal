import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Cell } from 'src/app/models/cell';

@Injectable({
  providedIn: 'root'
})

export class ShotHubService {

  public shots: Cell[];
  private hub: HubConnection;

  constructor() {
    this.hub = new HubConnectionBuilder().withUrl('http://localhost:54708/shothub').build();
    this.hub.start().then(() => console.log('Hub connected'))
    .catch(err => console.error(`Error for hub connection ${err}`));
    this.hub.on('getShots', (cells: Cell[]) => {
      this.shots = cells;
    });
   }

   receiveShots(gameId: string, playerId: string) {
     this.hub.send('getShots', gameId, playerId);
   }
}
