import { Game } from './../../models/game';
import { Player } from './../../models/player';
import { Cell } from './../../models/cell';
import { Component, OnInit } from '@angular/core';
import {ShipService} from '../../Services/ship.service'
import {EnemyService} from '../../Services/enemy.service'
import { fieldCell } from 'src/app/models/fieldCell';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { AuthService} from '../../auth/auth.component';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrls: ['./field.component.scss']
})
export class FieldComponent implements OnInit {
  Field: fieldCell[][];
  enemyField: fieldCell[][];
  alphabet: String[];
  numbers: number[];
  isActive: boolean = false;
  N = 10;
  constructor(private shipService: ShipService,
              private enemyService: EnemyService,
              private http: HttpClient,
              public authService : AuthService) {
    this.alphabet = [ ' ', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж' , 'З', 'И', 'К' ];
    this.numbers = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];
    authService.subscribe(this.getPlayer.bind(this));
   }

  ngOnInit() {
    this.Field = this.createField();
    this.enemyField = this.createField();
    this.enemyService.addField(this.enemyField); 
    this.shipService.addField(this.Field);
    this.getPlayer();
    this.shipService.getShips();
    this.shipService.getEnemyShots();
    //this.shipService
    this.enemyService.getShots();
  }

  createField(): fieldCell[][] {
    let field = [];
    for (let i = 0; i < this.N; i++) {
      field[i] = [];
      for (let j = 0; j < this.N; j++) {
          field[i][j] = new fieldCell(j, i, 0);
      }
    }
    return field;
  }
  startGame()
  {
    this.http.post<Game>(`${environment.base_url}api/StartGame`, "", {withCredentials: true}).subscribe(data => {
      console.log(`Message: Game start`);
      if (data != null && data !== undefined) {
        this.isActive = !data.finish;
        if(this.isActive)
        {
          this.shipService.SubscribeOnEnemyShot(data.id);
        }
      }
    });
  }

  shoot(x:number, y: number)
  {
    this.enemyService.hitTarget(x,y);
  }
  finishGame(){
    this.http.put<Game>(`${environment.base_url}api/FinishGame`, "", {withCredentials: true}).subscribe(data => {
      console.log(`Message: Game start`);
      if (data != null && data !== undefined) {
        this.isActive = !data.finish;
      }
      this.shipService.reset();
    });
  }

  getPlayer()
  {
    this.http.get<Player>(`${environment.base_url}api/GetPlayer`, {withCredentials: true}).subscribe(data => {
      console.log(`Message: Game start`);
      if (data != null && data !== undefined) {
        if(data.gameId !== null && data.gameId !== undefined && data.gameId !== "")
        {
          this.isActive = true;
          this.shipService.SubscribeOnEnemyShot(data.gameId);
        } else 
        {
          this.isActive = false;
        }
      }
    });
  }

}
