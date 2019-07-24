import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Cell } from 'src/app/models/cell';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ShotHubService {

  private hub: HubConnection;
  public subject: BehaviorSubject<Cell[]>;

  constructor() {
    this.subject = new BehaviorSubject(Array<Cell>());
    this.hub = new HubConnectionBuilder().withUrl('http://localhost:54708/shothub').build();
    this.hub.start().then(() => console.log('Hub connected'))
    .catch(err => console.error(`Error for hub connection ${err}`));
    this.hub.on('getShots', (cells: Cell[]) => {
      this.subject.next(cells);
    });
   }

   public receiveShots(): Observable<Cell[]> {
     return this.subject.asObservable();
   }
}
