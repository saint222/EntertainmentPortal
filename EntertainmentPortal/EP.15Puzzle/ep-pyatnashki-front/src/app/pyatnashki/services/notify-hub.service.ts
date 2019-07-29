import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotifyHubService {
  private hub: HubConnection;
  notification: string;
  constructor() {
    this.hub = new HubConnectionBuilder().withUrl(`${environment.api_url}/notice`).build();
    this.hub.start();

    this.hub.on('notice', (message: string) => {
      this.notify(message);
    });

   }

  public async notify(message) {
    this.notification = message;
    await new Promise(resolve => setTimeout(resolve, 3000));
    this.notification = null;
  }
}
