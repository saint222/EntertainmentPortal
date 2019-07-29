import { Player } from './models/player';
import { Component, OnInit } from '@angular/core';
import {ShipService} from './Services/ship.service';
import {EnemyService} from './Services/enemy.service';
import { fieldCell } from 'src/app/models/fieldCell';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment.prod';
import { AuthService} from './auth/auth.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
    isActive: boolean = false;
    player: Player[];
    constructor(private shipService: ShipService,
      private enemyService: EnemyService,
      private http: HttpClient,
      public authService : AuthService) {
    }

    ngOnInit() {
      this.getPlayer();
    }


    getPlayer()
    {
      this.http.get<Player>(`${environment.base_url}api/GetPlayer`, {withCredentials: true}).subscribe(data => {
        console.log(`Message: Game start`);
        if (data != null && data !== undefined) {
          if(data.nickName !== null && data.nickName !== undefined && data.nickName !== "")
          {
            this.isActive = true;
          } else 
          {
            this.isActive = false;
          }
        }
      });
    }

    addPlayer(nickName: string) {
      this.http.post<Player[]>(`${environment.base_url}api/AddPlayer`, {"nickName": nickName}, {withCredentials: true}).subscribe(data => {
        if (data != null && data !== undefined) {
          this.player = data;
          console.log(`Message: addPlayer`);
        }
      });
    }
}


