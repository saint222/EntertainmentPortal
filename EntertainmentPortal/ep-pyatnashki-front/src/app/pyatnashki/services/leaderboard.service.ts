import { environment } from 'src/environments/environment.prod';
import { Champion } from './../models/champion';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  private url = `${environment.api_url}/record`;
  constructor(private http: HttpClient) { }

  getRecords() {
    return this.http.get<Champion[]>(this.url, { withCredentials: true });
  }
}
