import { FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cell } from 'src/app/models/cell';
import { fieldCell } from 'src/app/models/fieldCell';
import { Ship } from 'src/app/models/ship';
import { Subscription } from 'rxjs/internal/Subscription';
import { environment } from '../../environments/environment.prod';
import { AuthService} from '../auth/auth.component'
import {Shot} from 'src/app/models/shot';
import { HubService } from '../hub.service';

@Injectable({
  providedIn: 'root'
})
export class ShipService {

  Field: fieldCell[][];
  ships: Ship[];
  subscription: Subscription;
  oneLast: number;
  twoLast: number;
  threeLast: number;
  fourLast: number;
  functionToUpdCalc: Function;
  N = 10;
  constructor(private http: HttpClient, public authService : AuthService, public demoHub: HubService) {
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
    this.ships = new Array<Ship>();
    this.authService.subscribe(this.getShips.bind(this));
    demoHub.SubscribeToGetEnemyShot(this.enemyShot.bind(this));
   }

   subscribeOnCalc(f: Function)
   {
    this.functionToUpdCalc= f;
   }

   addField(field: fieldCell[][])
   {
    this.Field = field;
   }

   enemyShot(userID:string, x:number, y:number)
   {
    console.log(`X: ${x} Y: ${y} EnemyUser: ${userID} UserId: ${this.authService.getValueFromIdToken("sub")}`);
       if(userID === this.authService.getValueFromIdToken("sub"))
       {
           this.Field[y][x].status = (this.Field[y][x].status == 1) ? 2 :
                                     (this.Field[y][x].status == 0) ? 4 : this.Field[y][x].status;
       }
   }
  addShip(form: FormGroup) {
    var ship = new Ship();
    var rank:number = form.value.rank;
    /*'{\n  "x": '+ form.value.x.toString() +
                                                                 ',\n  "y": '+ form.value.y.toString() +
                                                                 ',\n  "orientation": '+ form.value.orientation.toString() +
                                                                 ',\n  "rank": '+ rank.toString() + '\n}'*/

    this.http.post<Ship[]>(`${environment.base_url}api/addShip`, {'x': form.value.x-1,
                                                                  'y': form.value.y-1,
                                                                  'orientation': <number>form.value.orientation,
                                                                  'rank': <number>rank
                                                                }, {withCredentials: true}).subscribe(data => {
      console.log(`Message: addShip`);
      if (data != null && data !== undefined) {
        console.log(`Message: addShip DATA`);
        this.ships = data;
        this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
        this.calculateShipCountByRank(); // тут считаем количество кораблей
      }
    });
  }

  deleteShip(x: number, y: number) {
    let json = { x: x.toString(), y: y.toString() };
    this.http.delete<Ship[]>(`${environment.base_url}api/Ships/delete`, { params: json, withCredentials: true}).subscribe(data => {
      this.Field.forEach(row => row.forEach(cell => cell.status = 0));
      this.ships = data;
      data.forEach(ship => this.drawShip(ship));
      this.calculateShipCountByRank();
    });
  }

  getShips() {
    if((<ShipService>(this)).authService.isTokenValid())
    {
      this.http.get<Ship[]>(`${environment.base_url}api/GetShips`, {withCredentials: true}).subscribe(data => {
        console.log(`Message: addShip`);
        if (data != null && data !== undefined) {
          console.log(`Message: addShip DATA`);
          this.ships = data;
          this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
          this.calculateShipCountByRank(); // тут считаем количество кораблей
        }
      }  );
    }
  }

  getEnemyShots() {
    this.http.get<Shot[]>(`${environment.base_url}api/GetEnemyShots`, {withCredentials: true}).subscribe(data => {
      console.log(`Message: getEnemyShots`);
      if (data != null && data !== undefined) {
        console.log(`Message: getEnemyShots DATA`);
        data.forEach(shot => this.drawShot(shot)); // тут рисуем shots
      }
    }  );
}
  getShipField(): fieldCell[][] {
    return this.Field;
  }

  private drawShip(ship: Ship) {
    ship.cells.forEach(cell => {
      this.Field[cell.y][cell.x].status = (cell.isAlive == true) ? 1 : 2;
    });
  }

  private drawShot(shot: Shot) {
    this.Field[shot.y][shot.x].status = shot.status == 0 ? 4 :
                                        shot.status == 1 ? 2 : 2 ;
  }

  SubscribeOnEnemyShot(gameID:string){
    this.demoHub.Subscribe(gameID);
  }
  private calculateShipCountByRank() {
    let one = 0;
    let two = 0;
    let three = 0;
    let four = 0;
    this.ships.forEach(ship => {
      switch (ship.rank) {
        case 1:
          one += 1;
          break;
        case 2:
          two += 1;
          break;
        case 3:
          three += 1;
          break;
        case 4:
          four += 1;
          break;
      }
    });
    this.oneLast = 4 - one;
    this.twoLast = 3 - two;
    this.threeLast = 2 - three;
    this.fourLast = 1 - four;
    this.functionToUpdCalc(this.oneLast, this.twoLast, this.threeLast, this.fourLast);
  }

  reset() {
    this.oneLast = 4;
    this.twoLast = 3;
    this.threeLast = 2;
    this.fourLast = 1;
    this.ships = new Array<Ship>();
    this.ships.forEach(ship => this.drawShip(ship)); // тут рисуем корабли
    this.calculateShipCountByRank(); // тут считаем количество кораблей
    //this.http.get<any>(`${environment.base_url}api/game/new`, {params: {playerId : '1', gameId: '1'}, withCredentials: true}).subscribe();
  }
}