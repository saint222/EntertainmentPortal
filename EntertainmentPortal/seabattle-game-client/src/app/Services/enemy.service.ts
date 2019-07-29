import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { fieldCell } from 'src/app/models/fieldCell';
import { environment } from '../../environments/environment.prod';
import { AuthService} from '../auth/auth.component'
import {Shot} from 'src/app/models/shot';

@Injectable({
  providedIn: 'root'
})


export class EnemyService {
  Field: fieldCell[][];
  constructor(private http: HttpClient, public authService : AuthService) {
    this.authService.subscribe(this.getShots.bind(this));
   }

  hitTarget(x: number, y: number) {
    this.http.post<Shot>(`${environment.base_url}api/HitTarget`, {"x": x, "y" : y}, {withCredentials: true}).subscribe(data => {
      this.drawShot(data);
    });
  }



  getShots() {
      this.http.get<Shot[]>(`${environment.base_url}api/GetShots`, {withCredentials: true}).subscribe(data => {
        if (data != null && data !== undefined) {
          data.forEach(shot => this.drawShot(shot)); // тут рисуем shots
        }
      }  );
  }

  private drawShot(shot: Shot) {
    this.Field[shot.y][shot.x].status = shot.status == 0 ? 4 :
                                        shot.status == 1 ? 2 : 2 ;
  }
  addField(field: fieldCell[][])
  {
   this.Field = field;
  }

  reset() {
    
  }
}